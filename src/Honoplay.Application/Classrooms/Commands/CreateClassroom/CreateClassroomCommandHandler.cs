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
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common.Extensions;
using Microsoft.Data.Sqlite;

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
            var newClassroomTrainees = new List<ClassroomTrainee>();
            var createdClassrooms = new List<CreateClassroomModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createClassroomModel in request.CreateClassroomModels)
                    {
                        foreach (var traineeId in createClassroomModel.TraineesId)
                        {
                            var isExist = await _context.Trainees
                                .Include(x => x.Department)
                                .AnyAsync(x => x.Id == traineeId
                                               && x.Department.TenantId == request.TenantId, cancellationToken);
                            if (!isExist)
                            {
                                throw new NotFoundException(nameof(Trainee), traineeId);
                            }
                        }
                        var newClassroom = new Classroom
                        {
                            CreatedBy = request.CreatedBy,
                            TrainerUserId = createClassroomModel.TrainerUserId,
                            Name = createClassroomModel.Name,
                            TrainingId = createClassroomModel.TrainingId
                        };
                        newClassrooms.Add(newClassroom);
                    }

                    if (newClassrooms.Count > 20)
                    {
                        _context.BulkInsert(newClassrooms);
                        foreach (var newClassroom in newClassrooms)
                        {
                            newClassroomTrainees.AddRange(newClassroom.ClassroomTrainees.Select(newClassroomTrainee =>
                                new ClassroomTrainee
                                {
                                    ClassroomId = newClassroomTrainee.ClassroomId,
                                    TraineeId = newClassroomTrainee.TraineeId
                                }));
                            var a = newClassroomTrainees.Where(x => x.ClassroomId == newClassroom.Id).Select(x => x.TraineeId).ToList();
                        }

                        await _context.BulkInsertAsync(newClassroomTrainees, cancellationToken: cancellationToken);

                    }
                    else
                    {
                        await _context.Classrooms.AddRangeAsync(newClassrooms, cancellationToken);
                        newClassroomTrainees.ForEach(newClassroomTrainee => _context.ClassroomTrainees.Add(new ClassroomTrainee
                        {
                            ClassroomId = newClassroomTrainee.ClassroomId,
                            TraineeId = newClassroomTrainee.TraineeId
                        }));
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Classrooms
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newClassrooms.ForEach(x =>
                        createdClassrooms.Add(new CreateClassroomModel(x.Id,
                            x.TrainerUserId,
                            x.TrainingId,
                            x.Name,
                            x.ClassroomTrainees
                                .Select(s => s.TraineeId)
                                .ToList(),
                            x.CreatedBy,
                            x.CreatedAt)));
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(Department), ExceptionExtensions.GetExceptionMessage(ex));
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

            return new ResponseModel<List<CreateClassroomModel>>(createdClassrooms);
        }
    }
}
