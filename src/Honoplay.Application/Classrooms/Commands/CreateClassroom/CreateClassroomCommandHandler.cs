﻿using EFCore.BulkExtensions;
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
            var newClassroomTraineeUsers = new List<ClassroomTraineeUser>();
            var createdClassrooms = new List<CreateClassroomModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createClassroomModel in request.CreateClassroomModels)
                    {
                        foreach (var traineeUserId in createClassroomModel.TraineeUsersIdList)
                        {
                            var isExist = await _context.TraineeUsers
                                .Include(x => x.Department)
                                .AnyAsync(x => x.Id == traineeUserId
                                               && x.Department.TenantId == request.TenantId, cancellationToken);
                            if (!isExist)
                            {
                                throw new NotFoundException(nameof(TraineeUser), traineeUserId);
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
                        await _context.BulkInsertAsync(newClassrooms, cancellationToken: cancellationToken);


                        foreach (var newClassroom in newClassrooms)
                        {
                            foreach (var createClassroomModel in request.CreateClassroomModels)
                            {
                                foreach (var i in createClassroomModel.TraineeUsersIdList)
                                {
                                    newClassroomTraineeUsers.Add(new ClassroomTraineeUser
                                    {
                                        ClassroomId = newClassroom.Id,
                                        TraineeUserId = i
                                    });
                                }
                            }
                        }

                        await _context.BulkInsertAsync(newClassroomTraineeUsers, cancellationToken: cancellationToken);

                    }
                    else
                    {
                        for (int i = 0; i < newClassrooms.Count; i++)
                        {
                            await _context.Classrooms.AddAsync(newClassrooms[i], cancellationToken);
                            foreach (var traineeUserId in request.CreateClassroomModels[i].TraineeUsersIdList)
                            {
                                newClassrooms[i].ClassroomTraineeUsers.Add(new ClassroomTraineeUser
                                {
                                    TraineeUserId = traineeUserId
                                });
                            }
                        }

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
                            x.ClassroomTraineeUsers
                                .Select(s => s.TraineeUserId)
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
