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

namespace Honoplay.Application.TrainerUsers.Commands.UpdateTrainerUser
{
    public class UpdateTrainerUserCommandHandler : IRequestHandler<UpdateTrainerUserCommand, ResponseModel<UpdateTrainerUserModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTrainerUserCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateTrainerUserModel>> Handle(UpdateTrainerUserCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainerUsersWithDepartmentsByTenantId{request.TenantId}";
            var updateAt = DateTimeOffset.Now;
            var salt = ByteArrayExtensions.GetRandomSalt();


            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var trainerUsers = await _context.TrainerUsers
                        .Include(x => x.Department)
                        .Where(x => x.Department.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var trainerUser = trainerUsers.FirstOrDefault(x => x.Id == request.Id);

                    if (trainerUser is null)
                    {
                        throw new NotFoundException(nameof(trainerUser), request.Id);
                    }

                    trainerUser.Name = request.Name;
                    trainerUser.DepartmentId = request.DepartmentId;
                    trainerUser.PhoneNumber = request.PhoneNumber;
                    trainerUser.Surname = request.Surname;
                    trainerUser.UpdatedBy = request.UpdatedBy;
                    trainerUser.UpdatedAt = updateAt;
                    trainerUser.Email = request.Email;
                    trainerUser.PasswordSalt = salt;
                    trainerUser.Password = request.Password.GetSHA512(salt);
                    trainerUser.LastPasswordChangeDateTime = updateAt;
                    trainerUser.ProfessionId = request.ProfessionId;

                    _context.Update(trainerUser);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ =>
                            trainerUsers
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

            var trainerUserModel = new UpdateTrainerUserModel(request.Id,
                                                      updateAt,
                                                      request.Name,
                                                      request.Surname,
                                                      request.Email,
                                                      request.PhoneNumber,
                                                      request.DepartmentId,
                                                      request.ProfessionId);
            return new ResponseModel<UpdateTrainerUserModel>(trainerUserModel);
        }
    }
}
