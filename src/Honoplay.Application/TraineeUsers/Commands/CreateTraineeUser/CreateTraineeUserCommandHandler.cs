using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser
{
    public class CreateTraineeUserCommandHandler : IRequestHandler<CreateTraineeUserCommand, ResponseModel<CreateTraineeUserModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateTraineeUserCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<CreateTraineeUserModel>> Handle(CreateTraineeUserCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeUsersWithDepartmentsByTenantId{request.TenantId}";
            var traineeUser = new TraineeUser
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
                Gender = request.Gender,
                NationalIdentityNumber = request.NationalIdentityNumber,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname,
                WorkingStatusId = request.WorkingStatusId,
                CreatedBy = request.CreatedBy,
            };

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var isExistAnyDepartment = await _context.Departments.AnyAsync(x =>
                        x.TenantId == request.TenantId
                        && x.Id == request.DepartmentId,
                        cancellationToken);

                    if (!isExistAnyDepartment)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    await _context.TraineeUsers.AddAsync(traineeUser, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.TraineeUsers
                            .Include(x => x.Department)
                            .Where(x => x.Department.TenantId == request.TenantId)
                            .ToList()
                        , cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(TraineeUser), request.Name);
                }
                catch (NotFoundException)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var traineeUserModel = new CreateTraineeUserModel(traineeUser.Id,
                traineeUser.CreatedAt,
                traineeUser.Name, traineeUser.Surname, traineeUser.NationalIdentityNumber, traineeUser.PhoneNumber, traineeUser.Gender);
            return new ResponseModel<CreateTraineeUserModel>(traineeUserModel);
        }
    }
}
