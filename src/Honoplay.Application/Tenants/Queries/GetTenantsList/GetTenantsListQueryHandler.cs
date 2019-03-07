﻿using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQueryHandler : IRequestHandler<GetTenantsListQuery, ResponseModel<TenantsListModel>>
    {
        private readonly HonoplayDbContext _context;

        public GetTenantsListQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TenantsListModel>> Handle(GetTenantsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tenants.AsNoTracking().OrderBy(x => x.Name);

            var result = query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(TenantsListModel.Projection)
                .ToList();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TenantsListModel>(await query.CountAsync(), request.Take, result);
        }
    }
}