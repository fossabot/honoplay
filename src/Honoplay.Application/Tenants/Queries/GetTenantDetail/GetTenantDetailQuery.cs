﻿using Honoplay.Application.Infrastructure;
using MediatR;
using System;

#nullable enable

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQuery : IRequest<ResponseModel<TenantDetailModel>>
    {
        public GetTenantDetailQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}