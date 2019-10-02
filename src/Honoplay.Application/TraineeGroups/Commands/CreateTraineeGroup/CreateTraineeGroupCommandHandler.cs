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

namespace Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup
{
    public class CreateTraineeGroupCommandHandler : IRequestHandler<CreateTraineeGroupCommand, ResponseModel<List<CreateTraineeGroupModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateTraineeGroupCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateTraineeGroupModel>>> Handle(CreateTraineeGroupCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeGroupsByTenantId{request.TenantId}";
            var newTraineeGroups = new List<TraineeGroup>();
            var createdTraineeGroups = new List<CreateTraineeGroupModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createTraineeGroupModel in request.CreateTraineeGroupCommandModels)
                    {
                        var newTraineeGroup = new TraineeGroup
                        {
                            TenantId = request.TenantId,
                            CreatedBy = request.CreatedBy,
                            Name = createTraineeGroupModel.Name
                        };
                        newTraineeGroups.Add(newTraineeGroup);
                    }

                    if (newTraineeGroups.Count > 20)
                    {
                        _context.BulkInsert(newTraineeGroups);
                    }
                    else
                    {
                        await _context.TraineeGroups.AddRangeAsync(newTraineeGroups, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.TraineeGroups
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newTraineeGroups.ForEach(x => createdTraineeGroups.Add(new CreateTraineeGroupModel(x.Id,
                                                                                     x.Name,
                                                                                     x.CreatedBy,
                                                                                     x.CreatedAt
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

            return new ResponseModel<List<CreateTraineeGroupModel>>(createdTraineeGroups);
        }
    }
}
