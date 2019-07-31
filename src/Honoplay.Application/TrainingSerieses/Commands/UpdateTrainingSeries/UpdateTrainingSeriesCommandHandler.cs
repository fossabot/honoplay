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

namespace Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public class UpdateTrainingSeriesCommandHandler : IRequestHandler<UpdateTrainingSeriesCommand, ResponseModel<UpdateTrainingSeriesModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTrainingSeriesCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<UpdateTrainingSeriesModel>> Handle(UpdateTrainingSeriesCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingSeriesesByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var optionsByTenantId = await _context.TrainingSerieses
                        .Where(x => x.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);


                    var updateTrainingSeries = optionsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateTrainingSeries is null)
                    {
                        throw new NotFoundException(nameof(TrainingSeries), request.Id);
                    }

                    updateTrainingSeries.Name = request.Name;
                    updateTrainingSeries.UpdatedAt = updatedAt;
                    updateTrainingSeries.UpdatedBy = request.UpdatedBy;

                    _context.TrainingSerieses.Update(updateTrainingSeries);
                    await _context.SaveChangesAsync(cancellationToken);

                    optionsByTenantId = optionsByTenantId.Select(x => new TrainingSeries
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        UpdatedAt = x.UpdatedAt
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => optionsByTenantId,
                        cancellationToken);

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(TrainingSeries), request.Id);
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
            var updateTrainingSeriesModel = new UpdateTrainingSeriesModel(request.Id,
                                                                          request.Name,
                                                                          request.UpdatedBy,
                                                                          updatedAt);

            return new ResponseModel<UpdateTrainingSeriesModel>(updateTrainingSeriesModel);
        }
    }
}
