using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
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

#nullable enable

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
            var updatedAt = DateTime.Now;
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var entity = await _context.Tenants.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (entity == null)
                    {
                        throw new NotFoundException(nameof(Tenant), request.Id);
                    }

                    entity.Name = request.Name;
                    entity.HostName = request.HostName;
                    entity.Description = request.Description;
                    entity.Logo = request.Logo;
                    entity.UpdatedBy = request.UpdatedBy;
                    entity.UpdatedAt = updatedAt;

                    _context.Tenants.Update(entity);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException(nameof(Tenant), request.HostName);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var model = new UpdateTenantModel(id: request.Id,
                updatedBy: request.UpdatedBy,
                updatedAt: updatedAt,
                name: request.Name,
                description: request.Description,
                hostName: request.HostName,
                logo: request.Logo);

            return new ResponseModel<UpdateTenantModel>(model);
        }
    }
}