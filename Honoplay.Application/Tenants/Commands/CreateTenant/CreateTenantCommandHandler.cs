using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

#nullable enable

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
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var model = new CreateTenantModel(id: item.Id,
                createdBy: item.CreatedBy,
                createdAt: item.CreatedAt,
                name: item.Name,
                description: item.Description,
                hostName: item.HostName,
                logo: item.Logo);


            return new ResponseModel<CreateTenantModel>(model);
        }
    }
}