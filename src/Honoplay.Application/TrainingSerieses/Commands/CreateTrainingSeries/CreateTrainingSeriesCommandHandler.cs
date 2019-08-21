using EFCore.BulkExtensions;
using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries
{
    public class CreateTrainingSeriesCommandHandler : IRequestHandler<CreateTrainingSeriesCommand, ResponseModel<List<CreateTrainingSeriesModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateTrainingSeriesCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateTrainingSeriesModel>>> Handle(CreateTrainingSeriesCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingSeriesesByTenantId{request.TenantId}";
            var newTrainingSerieses = new List<TrainingSeries>();
            var createdTrainingSerieses = new List<CreateTrainingSeriesModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createTrainingSeriesModel in request.CreateTrainingSeriesModels)
                    {
                        var newTrainingSeries = new TrainingSeries
                        {
                            CreatedBy = request.CreatedBy,
                            Name = createTrainingSeriesModel.Name,
                            TenantId = request.TenantId
                        };
                        newTrainingSerieses.Add(newTrainingSeries);
                    }

                    if (newTrainingSerieses.Count > 20)
                    {
                        _context.BulkInsert(newTrainingSerieses);
                    }
                    else
                    {
                        await _context.TrainingSerieses.AddRangeAsync(newTrainingSerieses, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.TrainingSerieses
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newTrainingSerieses.ForEach(x =>
                        createdTrainingSerieses.Add(new CreateTrainingSeriesModel(x.Id, x.Name, x.CreatedBy, x.CreatedAt)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(TrainingSeries), ExceptionExtensions.GetExceptionMessage(ex));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new ResponseModel<List<CreateTrainingSeriesModel>>(createdTrainingSerieses);
        }
    }
}
