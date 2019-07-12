using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQuery : IRequest<ResponseModel<TenantsListModel>>
    {
        public GetTenantsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetTenantsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetTenantsListQueryModel : IRequest<ResponseModel<TenantsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}