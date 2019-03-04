using Honoplay.Application.Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, CreateTenantModel>
    {
        private readonly HonoplayDbContext _context;

        public CreateTenantCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<CreateTenantModel> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var item = new Tenant
            {
                Name = request.Name,
                Description =request.Description,
                HostName = request.HostName,
                Logo = request.Logo,     
                CreatedBy = request.CreatedBy
            };

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync(cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();
                    throw new ObjectAlreadyExistsException();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new CreateTenantModel(id: item.Id,
                                              createdBy: item.CreatedBy,
                                              createdAt: item.CreatedAt,
                                              name: item.Name,
                                              description: item.Description,
                                              hostName: item.HostName,
                                              logo: item.Logo);
        }
    }
}