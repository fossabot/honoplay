using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
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
            var allTenants = await _context.Tenants
                .Where(x => x.Id == request.TenantId)
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            var filteredTenants = allTenants
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .AsQueryable()
                .Select(TenantsListModel.Projection)
                .ToList();

            if (!filteredTenants.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TenantsListModel>(numberOfTotalItems: allTenants.LongCount(), numberOfSkippedItems: request.Take, source: filteredTenants);
        }
    }
}