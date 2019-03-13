using Honoplay.Application.Infrastructure;
using MediatR;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQuery : IRequest<ResponseModel<TenantsListModel>>
    {
        public GetTenantsListQuery()
        {

        }

        public GetTenantsListQuery(int adminUserId) : this(adminUserId, 0, 10)
        {
        }

        public GetTenantsListQuery(int adminUserId, int skip, int take)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public int? AdminUserId { get; set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
    }

    public class GetTenantsListQueryModel : IRequest<ResponseModel<TenantsListModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}