using EFCore.BulkExtensions;
using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainings.Commands.CreateTraining
{
    public class CreateTrainingCommandHandler : IRequestHandler<CreateTrainingCommand, ResponseModel<List<CreateTrainingModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateTrainingCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateTrainingModel>>> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingsByTenantId{request.TenantId}";
            var newTrainings = new List<Training>();
            var createdTrainings = new List<CreateTrainingModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createTrainingModel in request.CreateTrainingModels)
                    {
                        var newTraining = new Training
                        {
                            CreatedBy = request.CreatedBy,
                            TrainingSeriesId = createTrainingModel.TrainingSeriesId,
                            Name = createTrainingModel.Name,
                            Description = createTrainingModel.Description,
                            BeginDateTime = createTrainingModel.BeginDateTime,
                            EndDateTime = createTrainingModel.EndDateTime,
                            TrainingCategoryId = createTrainingModel.TrainingCategoryId
                        };
                        newTrainings.Add(newTraining);
                    }

                    if (newTrainings.Count > 20)
                    {
                        _context.BulkInsert(newTrainings);
                    }
                    else
                    {
                        await _context.Trainings.AddRangeAsync(newTrainings, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Trainings
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newTrainings.ForEach(x =>
                        createdTrainings.Add(new CreateTrainingModel(x.Id, x.TrainingSeriesId, x.TrainingCategoryId, x.Name, x.Description, x.CreatedBy, x.CreatedAt, x.BeginDateTime, x.EndDateTime)));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new ResponseModel<List<CreateTrainingModel>>(createdTrainings);
        }
    }
}
