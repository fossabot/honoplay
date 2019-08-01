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

namespace Honoplay.Application.Trainings.Commands.UpdateTraining
{
    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand, ResponseModel<UpdateTrainingModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTrainingCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<UpdateTrainingModel>> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingsByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var trainingsByTenantId = await _context.Trainings
                        .Include(x => x.TrainingSeries)
                        .Where(x => x.TrainingSeries.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);


                    var updateTraining = trainingsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateTraining is null)
                    {
                        throw new NotFoundException(nameof(Training), request.Id);
                    }

                    updateTraining.Name = request.Name;
                    updateTraining.UpdatedAt = updatedAt;
                    updateTraining.UpdatedBy = request.UpdatedBy;

                    _context.Trainings.Update(updateTraining);
                    await _context.SaveChangesAsync(cancellationToken);

                    trainingsByTenantId = trainingsByTenantId.Select(x => new Training
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        UpdatedAt = x.UpdatedAt,
                        TrainingCategoryId = x.TrainingCategoryId,
                        BeginDateTime = x.BeginDateTime,
                        EndDateTime = x.EndDateTime,
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => trainingsByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Training), request.Id);
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
            var updateTrainingModel = new UpdateTrainingModel(request.Id,
                                                              request.TrainingSeriesId,
                                                              request.TrainingCategoryId,
                                                              request.Name,
                                                              request.Description,
                                                              request.UpdatedBy,
                                                              updatedAt,
                                                              request.BeginDateTime,
                                                              request.EndDateTime);

            return new ResponseModel<UpdateTrainingModel>(updateTrainingModel);
        }
    }
}
