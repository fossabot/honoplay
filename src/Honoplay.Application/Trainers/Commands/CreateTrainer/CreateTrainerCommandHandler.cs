using Honoplay.Application._Infrastructure;
using MediatR;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Honoplay.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandHandler : IRequestHandler<CreateTrainerCommand, ResponseModel<CreateTrainerModel>>
    {
        private readonly HonoplayDbContext _context;

        public CreateTrainerCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<CreateTrainerModel>> Handle(CreateTrainerCommand request,
            CancellationToken cancellationToken)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var department = await _context.Departments.FirstOrDefaultAsync(x =>
                            x.Id == request.DepartmentId,
                            cancellationToken);

                    var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                            x.AdminUserId == request.CreatedBy &&
                            x.TenantId == department.TenantId,
                            cancellationToken);

                    if (!isExist)
                    {
                        throw new NotFoundException(nameof(Department), request.DepartmentId);
                    }

                    var trainer = new Trainer
                    {
                        Name = request.Name,
                        CreatedBy = request.CreatedBy,
                        DepartmentId = request.DepartmentId,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        ProfessionId = request.ProfessionId,
                        Surname = request.Surname,
                    };

                    await _context.AddAsync(trainer, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();

                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException &&
                                                    (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException &&
                                                    sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Trainer), request.Name);
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

                var trainerModel = new CreateTrainerModel(
                    request.Name,
                    request.Surname,
                    request.Email,
                    request.PhoneNumber,
                    request.DepartmentId,
                    request.ProfessionId);

                return new ResponseModel<CreateTrainerModel>(trainerModel);
            }
        }
    }
}
