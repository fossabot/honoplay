using Honoplay.Application.Infrastructure;
using MediatR;



namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQuery : IRequest<ResponseModel<TenantsListModel>>
    {
        public GetTenantsListQuery() : this(0, 10)
        {

        }
        public GetTenantsListQuery(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}