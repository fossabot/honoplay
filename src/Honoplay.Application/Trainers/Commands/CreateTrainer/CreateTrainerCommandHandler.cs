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

namespace Honoplay.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandHandler : IRequestHandler<CreateTrainerCommand, ResponseModel<CreateTrainerModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateTrainerCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateTrainerModel>> Handle(CreateTrainerCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByTenantId{request.TenantId}";
            var trainer = new Trainer
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
                    var isExistAnyDepartment = await _context.Departments.AnyAsync(x =>
                            x.TenantId == request.TenantId
                            && x.Id == request.DepartmentId,
                        cancellationToken);

                    if (!isExistAnyDepartment)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    await _context.AddAsync(trainer, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey, delegate
                    {
                        return _context.Trainers.Include(x => x.Department)
                            .Where(x => x.Department.TenantId == request.TenantId)
                            .ToList();
                    }, cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
                                                   || (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Trainer), request.Name);
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

                var trainerModel = new CreateTrainerModel(
                    trainer.Id,
                    trainer.CreatedAt,
                    trainer.Name,
                    trainer.Surname,
                    trainer.Email,
                    trainer.PhoneNumber,
                    trainer.DepartmentId,
                    trainer.ProfessionId);

                return new ResponseModel<CreateTrainerModel>(trainerModel);
            }
        }
    }
}
