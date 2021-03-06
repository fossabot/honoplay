﻿using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQuery : IRequest<ResponseModel<TenantDetailModel>>
    {
        public GetTenantDetailQuery(int adminUserId, Guid id)
        {
            Id = id;
            AdminUserId = adminUserId;
        }

        public GetTenantDetailQuery() { }

        public Guid Id { get; private set; }
        public int AdminUserId { get; private set; }
    }
}