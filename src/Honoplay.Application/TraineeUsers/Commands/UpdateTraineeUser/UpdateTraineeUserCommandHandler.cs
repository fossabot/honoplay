using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
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

namespace Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser
{
    public class UpdateTraineeUserCommandHandler : IRequestHandler<UpdateTraineeUserCommand, ResponseModel<UpdateTraineeUserModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public UpdateTraineeUserCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<UpdateTraineeUserModel>> Handle(UpdateTraineeUserCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeUsersWithDepartmentsByTenantId{request.TenantId}";
            var salt = ByteArrayExtensions.GetRandomSalt();
            var updateAt = DateTimeOffset.Now;

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var traineeUsers = await _context.TraineeUsers
                        .Include(x => x.Department)
                        .Where(x => x.Department.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var traineeUser = traineeUsers.FirstOrDefault(x => x.Id == request.Id);

                    if (traineeUser is null)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    traineeUser.Name = request.Name;
                    traineeUser.DepartmentId = request.DepartmentId;
                    traineeUser.AvatarId = request.AvatarId;
                    traineeUser.Gender = request.Gender;
                    traineeUser.NationalIdentityNumber = request.NationalIdentityNumber;
                    traineeUser.PhoneNumber = request.PhoneNumber;
                    traineeUser.Surname = request.Surname;
                    traineeUser.WorkingStatusId = request.WorkingStatusId;
                    traineeUser.UpdatedBy = request.UpdatedBy;
                    traineeUser.UpdatedAt = updateAt;
                    traineeUser.PasswordSalt = salt;
                    traineeUser.Password = request.Password.GetSHA512(salt);
                    traineeUser.LastPasswordChangeDateTime = updateAt;

                    _context.Update(traineeUser);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => traineeUsers
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

            var traineeUserModel = new UpdateTraineeUserModel(request.Id,
                                                      request.Name,
                                                      request.Email,
                                                      request.Surname,
                                                      request.NationalIdentityNumber,
                                                      request.PhoneNumber,
                                                      request.Gender,
                                                      request.AvatarId,
                                                      updateAt);
            return new ResponseModel<UpdateTraineeUserModel>(traineeUserModel);
        }
    }
}
