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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupCommandHandler : IRequestHandler<UpdateTraineeGroupCommand, ResponseModel<List<UpdateTraineeGroupModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateTraineeGroupCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<UpdateTraineeGroupModel>>> Handle(UpdateTraineeGroupCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeGroupsByTenantId{request.TenantId}";
            var updateTraineeGroups = new List<TraineeGroup>();
            var updatedTraineeGroups = new List<UpdateTraineeGroupModel>();

            var redisTraineeGroups = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeGroups
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                    .ToList()
                , cancellationToken);


            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var updateTraineeGroupModel in request.UpdateTraineeGroupCommandModels)
                    {
                        var traineeGroup = redisTraineeGroups.FirstOrDefault(x => x.Id == updateTraineeGroupModel.Id);
                        if (traineeGroup != null) throw new TransactionException();

                        var updateTraineeGroup = new TraineeGroup
                        {
                            TenantId = request.TenantId,
                            UpdatedBy = request.UpdatedBy,
                            Name = updateTraineeGroupModel.Name
                        };
                        updateTraineeGroups.Add(updateTraineeGroup);
                    }

                    if (updateTraineeGroups.Count > 20)
                    {
                        _context.BulkUpdate(updateTraineeGroups);
                    }
                    else
                    {
                        await _context.TraineeGroups.AddRangeAsync(updateTraineeGroups, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.TraineeGroups
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    updateTraineeGroups.ForEach(x => updatedTraineeGroups.Add(new UpdateTraineeGroupModel(x.Id,
                                                                                     x.Name,
                                                                                     x.UpdatedBy,
                                                                                     x.UpdatedAt
                                                                                     )));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(TraineeGroup), ExceptionExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<UpdateTraineeGroupModel>>(updatedTraineeGroups);
        }
    }
}
