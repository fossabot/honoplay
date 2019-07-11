using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Commands.UpdateTrainee
{
    public class UpdateTraineeCommandHandler : IRequestHandler<UpdateTraineeCommand, ResponseModel<UpdateTraineeModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public UpdateTraineeCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<UpdateTraineeModel>> Handle(UpdateTraineeCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";
            var updateAt = DateTimeOffset.Now;

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var trainees = await _context.Trainees.Include(x => x.Department)
                        .Where(x => x.Department.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var trainee = trainees.FirstOrDefault(x => x.Id == request.Id);

                    if (trainee is null)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    trainee.Name = request.Name;
                    trainee.DepartmentId = request.DepartmentId;
                    trainee.Gender = request.Gender;
                    trainee.NationalIdentityNumber = request.NationalIdentityNumber;
                    trainee.PhoneNumber = request.PhoneNumber;
                    trainee.Surname = request.Surname;
                    trainee.WorkingStatusId = request.WorkingStatusId;
                    trainee.UpdatedBy = request.UpdatedBy;
                    trainee.UpdatedAt = updateAt;

                    _context.Update(trainee);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                     {
                         return trainees;
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

            var traineeModel = new UpdateTraineeModel(request.Id, request.Name, request.Surname, request.NationalIdentityNumber, request.PhoneNumber, request.Gender, updateAt);
            return new ResponseModel<UpdateTraineeModel>(traineeModel);
        }
    }
}
