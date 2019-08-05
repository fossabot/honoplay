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

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, ResponseModel<List<CreateClassroomModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateClassroomCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateClassroomModel>>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"ClassroomsByTenantId{request.TenantId}";
            var newClassrooms = new List<Classroom>();
            var createdClassrooms = new List<CreateClassroomModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createClassroomModel in request.CreateClassroomModels)
                    {
                        var newClassroom = new Classroom
                        {
                            CreatedBy = request.CreatedBy,
                            TrainerId = createClassroomModel.TrainerId,
                            Name = createClassroomModel.Name,
                            TrainingId = createClassroomModel.TrainingId
                        };
                        newClassrooms.Add(newClassroom);
                    }

                    if (newClassrooms.Count > 20)
                    {
                        _context.BulkInsert(newClassrooms);
                    }
                    else
                    {
                        await _context.Classrooms.AddRangeAsync(newClassrooms, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Classrooms
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newClassrooms.ForEach(x =>
                        createdClassrooms.Add(new CreateClassroomModel(x.Id, x.TrainerId, x.TrainingId, x.Name, x.CreatedBy, x.CreatedAt)));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new ResponseModel<List<CreateClassroomModel>>(createdClassrooms);
        }
    }
}
