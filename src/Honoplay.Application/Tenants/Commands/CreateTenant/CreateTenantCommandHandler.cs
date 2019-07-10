using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
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


namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ResponseModel<CreateTenantModel>>
    {
        private readonly HonoplayDbContext _context;

        public CreateTenantCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<CreateTenantModel>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var addTenant = new Tenant
            {
                Name = request.Name,
                Description = request.Description,
                HostName = request.HostName,
                Logo = request.Logo,
                CreatedBy = request.CreatedBy
            };

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.Tenants.AddAsync(addTenant, cancellationToken);

                    await _context.TenantAdminUsers.AddAsync(new TenantAdminUser
                    {
                        TenantId = addTenant.Id,
                        AdminUserId = request.CreatedBy,
                        CreatedBy = request.CreatedBy
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Tenant), request.HostName);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var model = new CreateTenantModel(id: addTenant.Id,
                createdAt: addTenant.CreatedAt,
                name: addTenant.Name,
                description: addTenant.Description,
                hostName: addTenant.HostName,
                logo: addTenant.Logo);

            return new ResponseModel<CreateTenantModel>(model);
        }
    }
}