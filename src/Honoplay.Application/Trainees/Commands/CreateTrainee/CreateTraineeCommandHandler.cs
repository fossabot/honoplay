using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeCommandHandler : IRequestHandler<CreateTraineeCommand, ResponseModel<CreateTraineeModel>>
    {
        private readonly HonoplayDbContext _context;
        public CreateTraineeCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<CreateTraineeModel>> Handle(CreateTraineeCommand request, CancellationToken cancellationToken)
        {
            var trainee = new Trainee
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
                Gender = request.Gender,
                NationalIdentityNumber = request.NationalIdentityNumber,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname,
                WorkingStatusId = request.WorkingStatusId,
                CreatedBy = request.AdminUserId,
            };

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var department = await _context.Departments.SingleOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
                    var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                                                                    x.AdminUserId == request.AdminUserId
                                                                    && x.TenantId == department.TenantId,
                                                                    cancellationToken);
                    if (!isExist)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    await _context.AddAsync(trainee, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(trainee), request.Name);
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

            var traineeModel = new CreateTraineeModel(request.Name, request.Surname, request.NationalIdentityNumber, request.PhoneNumber, request.Gender);
            return new ResponseModel<CreateTraineeModel>(traineeModel);
        }
    }
}
