using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
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
using EFCore.BulkExtensions;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomCommandHandler : IRequestHandler<UpdateClassroomCommand, ResponseModel<UpdateClassroomModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateClassroomCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<UpdateClassroomModel>> Handle(UpdateClassroomCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"ClassroomsByTenantId{request.TenantId}";
            var updatedAt = DateTimeOffset.Now;
            var updateClassroomTraineeUsers = new List<ClassroomTraineeUser>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var classroomsByTenantId = await _context.Classrooms
                        .Include(x => x.Training)
                        .Include(x => x.Training.TrainingSeries)
                        .Where(x => x.Training.TrainingSeries.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    var updateClassroom = classroomsByTenantId.FirstOrDefault(x => x.Id == request.Id);
                    if (updateClassroom is null)
                    {
                        throw new NotFoundException(nameof(Classroom), request.Id);
                    }

                    updateClassroom.Name = request.Name;
                    updateClassroom.UpdatedAt = updatedAt;
                    updateClassroom.UpdatedBy = request.UpdatedBy;

                    foreach (var i in request.TraineeUsersIdList)
                    {
                        updateClassroomTraineeUsers.Add(new ClassroomTraineeUser
                        {
                            ClassroomId = request.Id,
                            TraineeUserId = i
                        });
                    }
                    updateClassroom.ClassroomTraineeUsers = updateClassroomTraineeUsers;

                    _context.UpdateRange(updateClassroomTraineeUsers);

                    _context.Classrooms.Update(updateClassroom);
                    await _context.SaveChangesAsync(cancellationToken);

                    classroomsByTenantId = classroomsByTenantId.Select(x => new Classroom
                    {
                        Id = x.Id,
                        CreatedBy = x.CreatedBy,
                        UpdatedBy = x.UpdatedBy,
                        Name = x.Name,
                        UpdatedAt = x.UpdatedAt,
                        TrainingId = x.TrainingId,
                        TrainerUserId = x.TrainerUserId
                    }).ToList();

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => classroomsByTenantId,
                        cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Classroom), request.Id);
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
            var updateClassroomModel = new UpdateClassroomModel(request.Id,
                                                              request.TrainerUserId,
                                                              request.TrainingId,
                                                              request.Name,
                                                              request.TraineeUsersIdList,
                                                              request.UpdatedBy,
                                                              updatedAt);

            return new ResponseModel<UpdateClassroomModel>(updateClassroomModel);
        }
    }
}
