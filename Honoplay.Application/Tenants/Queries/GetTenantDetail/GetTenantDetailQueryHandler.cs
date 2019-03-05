using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQueryHandler : IRequestHandler<GetTenantDetailQuery, TenantDetailModel>
    {
        private readonly HonoplayDbContext _context;

        public GetTenantDetailQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<TenantDetailModel> Handle(GetTenantDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tenants.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tenant), request.Id);
            }

            return TenantDetailModel.Create(entity);
        }
    }
}