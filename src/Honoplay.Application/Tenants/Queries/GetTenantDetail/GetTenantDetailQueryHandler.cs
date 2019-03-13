using System.Linq;
using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQueryHandler : IRequestHandler<GetTenantDetailQuery, ResponseModel<TenantDetailModel>>
    {
        private readonly HonoplayDbContext _context;

        public GetTenantDetailQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TenantDetailModel>> Handle(GetTenantDetailQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.Id && x.TenantAdminUsers.Any(y => y.AdminUserId == request.AdminUserId), cancellationToken);

            if (tenant is null)
            {
                throw new NotFoundException(nameof(Tenant), request.Id);
            }

            var model = TenantDetailModel.Create(tenant);
            return new ResponseModel<TenantDetailModel>(model);
        }
    }
}