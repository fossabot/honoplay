using MediatR;
using System;

#nullable enable

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQuery : IRequest<TenantDetailModel>
    {
        public Guid Id { get; set; }
    }
}