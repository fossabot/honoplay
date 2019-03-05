using Honoplay.Application.Exceptions;
using Honoplay.Application.Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
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
            var entity = await _context.Tenants.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tenant), request.Id);
            }

            var model = TenantDetailModel.Create(entity);
            return new ResponseModel<TenantDetailModel>(model);
        }
    }
}