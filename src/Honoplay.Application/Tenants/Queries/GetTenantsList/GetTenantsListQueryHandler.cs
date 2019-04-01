using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;

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
            var query = _context.Tenants.Where(x => x.TenantAdminUsers.Any(y => y.AdminUserId == request.AdminUserId)).AsNoTracking().OrderBy(x => x.Name);

            var result = query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(TenantsListModel.Projection)
                .ToList();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TenantsListModel>(numberOfTotalItems: await query.CountAsync(cancellationToken), numberOfSkippedItems: request.Take, result);
        }
    }
}