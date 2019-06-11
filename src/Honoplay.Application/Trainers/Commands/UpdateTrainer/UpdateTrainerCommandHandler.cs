using Honoplay.Common._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Persistence.CacheService;

namespace Honoplay.Application.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerCommandHandler : IRequestHandler<UpdateTrainerCommand, ResponseModel<UpdateTrainerModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTrainerCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateTrainerModel>> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByHostName{request.HostName}";

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var trainers = await _context.Trainers.Include(x => x.Department)
                        .Where(x => _context.TenantAdminUsers.Any(y =>
                                         y.TenantId == x.Department.TenantId &&
                                         y.AdminUserId == request.UpdatedBy))
                        .ToListAsync(cancellationToken);

                    var trainer = trainers.FirstOrDefault(x => x.Id == request.Id);

                    if (trainer is null)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    trainer.Name = request.Name;
                    trainer.DepartmentId = request.DepartmentId;
                    trainer.PhoneNumber = request.PhoneNumber;
                    trainer.Surname = request.Surname;
                    trainer.UpdatedBy = request.UpdatedBy;
                    trainer.UpdatedAt = DateTimeOffset.Now;
                    trainer.Email = request.Email;
                    trainer.ProfessionId = request.ProfessionId;

                    _context.Update(trainer);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                    {
                        return trainers;
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

            var trainerModel = new UpdateTrainerModel(request.Id, request.Name, request.Surname, request.Email, request.PhoneNumber, request.DepartmentId, request.ProfessionId);
            return new ResponseModel<UpdateTrainerModel>(trainerModel);
        }
    }
}
