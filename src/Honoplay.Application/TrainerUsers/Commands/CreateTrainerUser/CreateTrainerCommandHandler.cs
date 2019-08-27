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

namespace Honoplay.Application.TrainerUsers.Commands.CreateTrainerUser
{
    public class CreateTrainerUserCommandHandler : IRequestHandler<CreateTrainerUserCommand, ResponseModel<CreateTrainerUserModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateTrainerUserCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateTrainerUserModel>> Handle(CreateTrainerUserCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainerUsersWithDepartmentsByTenantId{request.TenantId}";
            var trainerUser = new TrainerUser
            {
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                DepartmentId = request.DepartmentId,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ProfessionId = request.ProfessionId,
                Surname = request.Surname,
            };

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var isExistAnyDepartment = await _context.Departments
                        .AnyAsync(x =>
                            x.TenantId == request.TenantId
                            && x.Id == request.DepartmentId,
                        cancellationToken);

                    if (!isExistAnyDepartment)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    await _context.AddAsync(trainerUser, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ =>
                            _context.TrainerUsers.Include(x => x.Department)
                                .Where(x => x.Department.TenantId == request.TenantId)
                                .ToList()
                        , cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                                                   || (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(TrainerUser), request.Name);
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

                var trainerUserModel = new CreateTrainerUserModel(
                    trainerUser.Id,
                    trainerUser.CreatedAt,
                    trainerUser.Name,
                    trainerUser.Surname,
                    trainerUser.Email,
                    trainerUser.PhoneNumber,
                    trainerUser.DepartmentId,
                    trainerUser.ProfessionId);

                return new ResponseModel<CreateTrainerUserModel>(trainerUserModel);
            }
        }
    }
}
