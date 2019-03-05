﻿using Honoplay.Application.Infrastructure;
using MediatR;

#nullable enable

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQuery : IRequest<ResponseModel<TenantsListModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}