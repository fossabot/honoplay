﻿using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class UpdateTraineeCommandHandler : IRequestHandler<UpdateTraineeCommand, ResponseModel<UpdateTraineeModel>>
    {
        private readonly HonoplayDbContext _context;
        public UpdateTraineeCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<UpdateTraineeModel>> Handle(UpdateTraineeCommand request, CancellationToken cancellationToken)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var trainee = await _context.Trainees.Include(x => x.Department)
                        .Where(x => x.Id == request.Id &&
                                    _context.TenantAdminUsers.Any(y =>
                                        y.TenantId == x.Department.TenantId &&
                                        y.AdminUserId == request.UpdatedBy))
                        .FirstOrDefaultAsync(cancellationToken);

                    if (trainee is null)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    trainee.Name = request.Name;
                    trainee.DepartmentId = request.DepartmentId;
                    trainee.Gender = request.Gender;
                    trainee.NationalIdentityNumber = request.NationalIdentityNumber;
                    trainee.PhoneNumber = request.PhoneNumber;
                    trainee.Surname = request.Surname;
                    trainee.WorkingStatusId = request.WorkingStatusId;
                    trainee.UpdatedBy = request.UpdatedBy;
                    trainee.UpdatedAt = DateTimeOffset.Now;

                    _context.Update(trainee);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Trainee), request.Name);
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

            var traineeModel = new UpdateTraineeModel(request.Id, request.Name, request.Surname, request.NationalIdentityNumber, request.PhoneNumber, request.Gender);
            return new ResponseModel<UpdateTraineeModel>(traineeModel);
        }
    }
}
