using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, ResponseModel<UpdateTenantModel>>
    {
        private readonly HonoplayDbContext _context;

        public UpdateTenantCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<UpdateTenantModel>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var updatedAt = DateTimeOffset.Now;
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var tenant = await _context.Tenants.SingleOrDefaultAsync(x =>
                        x.Id == request.Id
                        && x.TenantAdminUsers.Any(y =>
                            y.AdminUserId == request.UpdatedBy),
                        cancellationToken);

                    if (tenant is null)
                    {
                        throw new NotFoundException(nameof(Tenant), request.Id);
                    }

                    tenant.Name = request.Name;
                    tenant.HostName = request.HostName;
                    tenant.Description = request.Description;
                    tenant.Logo = request.Logo;
                    tenant.UpdatedBy = request.UpdatedBy;
                    tenant.UpdatedAt = updatedAt;

                    _context.Tenants.Update(tenant);
                    await _context.SaveChangesAsync(cancellationToken);

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

            var model = new UpdateTenantModel(id: request.Id,
                updatedAt: updatedAt,
                name: request.Name,
                description: request.Description,
                hostName: request.HostName,
                logo: request.Logo);

            return new ResponseModel<UpdateTenantModel>(model);
        }
    }
}