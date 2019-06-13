using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
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
            var addDepartmentList = new List<Department>();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                var redisKey = $"DepartmentsByHostName{request.HostName}";
                try
                {
                    var currentTenant = await _context.Tenants.Include(x => x.Departments)
                        .FirstOrDefaultAsync(x =>
                                x.HostName == request.HostName
                                && x.TenantAdminUsers.Any(y =>
                                    y.AdminUserId == request.AdminUserId),
                            cancellationToken);

                    if (currentTenant is null)
                    {
                        throw new NotFoundException(nameof(Tenant), request.HostName);
                    }

                    foreach (var requestDepartment in request.Departments)
                    {
                        var department = new Department
                        {
                            CreatedBy = request.AdminUserId,
                            Name = requestDepartment,
                            TenantId = currentTenant.Id

                        };
                        addDepartmentList.Add(department);
                    }

                    await _context.Departments.AddRangeAsync(addDepartmentList, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        delegate
                         {
                             return currentTenant.Departments.ToList();
                         },
                        cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Tenant), request.HostName);
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

