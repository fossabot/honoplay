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
            var redisKey = $"TraineesWithDepartmentsByHostName{request.HostName}";

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var isExist = await _context.Departments.AnyAsync(x =>
                            x.Id == request.DepartmentId &&
                            _context.TenantAdminUsers.Any(y =>
                                y.AdminUserId == request.CreatedBy &&
                                y.TenantId == x.TenantId)
                        , cancellationToken);

                    if (!isExist)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

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

                    await _context.AddAsync(trainee, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                    {
                        return _context.Trainees.Include(x => x.Department)
                            .Where(x => _context.TenantAdminUsers.Any(y =>
                                            y.TenantId == x.Department.TenantId &&
                                            y.AdminUserId == request.CreatedBy))
                            .ToList();
                    }, cancellationToken);

                    transaction.Commit();
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

            var traineeModel = new CreateTraineeModel(request.Name, request.Surname, request.NationalIdentityNumber, request.PhoneNumber, request.Gender);
            return new ResponseModel<CreateTraineeModel>(traineeModel);
        }
    }
}
