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

namespace Honoplay.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ResponseModel<CreateDepartmentModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateDepartmentCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateDepartmentModel>> Handle(CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"DepartmentsByTenantId{request.TenantId}";
            var newDepartments = new List<Department>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var requestDepartment in request.Departments)
                    {
                        var department = new Department
                        {
                            CreatedBy = request.AdminUserId,
                            Name = requestDepartment,
                            TenantId = request.TenantId

                        };
                        newDepartments.Add(department);
                    }

                    await _context.Departments.AddRangeAsync(newDepartments, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var departmentsByTenantId = await _context.Departments
                        .Where(x => x.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    transaction.Commit();

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => departmentsByTenantId,
                        cancellationToken);
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Department), request.Departments);
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

            var departments = new CreateDepartmentModel(departments: newDepartments);
            return new ResponseModel<CreateDepartmentModel>(departments);
        }
    }
}

