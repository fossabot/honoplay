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

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeCommandHandler : IRequestHandler<CreateTraineeCommand, ResponseModel<CreateTraineeModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateTraineeCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<CreateTraineeModel>> Handle(CreateTraineeCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";
            var trainee = new Trainee
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

                    await _context.Trainees.AddAsync(trainee, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Trainees
                            .Include(x => x.Department)
                            .Where(x => x.Department.TenantId == request.TenantId)
                            .ToList()
                        , cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Trainee), request.Name);
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

            var traineeModel = new CreateTraineeModel(trainee.Id,
                trainee.CreatedAt,
                trainee.Name, trainee.Surname, trainee.NationalIdentityNumber, trainee.PhoneNumber, trainee.Gender);
            return new ResponseModel<CreateTraineeModel>(traineeModel);
        }
    }
}
