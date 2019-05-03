using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, ResponseModel<AddDepartmentModel>>
    {
        private readonly HonoplayDbContext _context;

        public AddDepartmentCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AddDepartmentModel>> Handle(AddDepartmentCommand request,
            CancellationToken cancellationToken)
        {

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var isExist = await _context.TenantAdminUsers.AnyAsync(
                        x => x.TenantId == request.TenantId && x.AdminUserId == request.AdminUserId, cancellationToken);
                    if (!isExist)
                    {
                        throw new NotFoundException(nameof(Tenant), request.TenantId);
                    }

                    foreach (var requestDepartment in request.Departments)
                    {
                        requestDepartment.CreatedBy = request.AdminUserId;
                    }

                    await _context.Departments.AddRangeAsync(request.Departments, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Tenant), request.TenantId);
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

            var departments = new AddDepartmentModel(departments: request.Departments);
            return new ResponseModel<AddDepartmentModel>(departments);
        }
    }
}

