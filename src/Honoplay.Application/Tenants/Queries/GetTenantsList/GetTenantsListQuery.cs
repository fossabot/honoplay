using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQuery : IRequest<ResponseModel<TenantsListModel>>
    {
        public GetTenantsListQuery()
        {
        }

        public GetTenantsListQuery(int adminUserId, int skip = 0, int take = 10)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }
        [JsonIgnore]
        public int? AdminUserId { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; }= 10;
    }

    public class GetTenantsListQueryModel : IRequest<ResponseModel<TenantsListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}