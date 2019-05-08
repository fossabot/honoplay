using System;
using System.Collections.Generic;
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
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ResponseModel<CreateDepartmentModel>>
    {
        private readonly HonoplayDbContext _context;

        public CreateDepartmentCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<CreateDepartmentModel>> Handle(CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var addDepartmentList = new List<Department>();

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
                        var department = new Department
                        {
                            CreatedBy = request.AdminUserId,
                            Name = requestDepartment,
                            TenantId = request.TenantId,

                        };
                        addDepartmentList.Add(department);
                    }

                    await _context.Departments.AddRangeAsync(addDepartmentList, cancellationToken);
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

            var departments = new CreateDepartmentModel(departments: addDepartmentList);
            return new ResponseModel<CreateDepartmentModel>(departments);
        }
    }
}

