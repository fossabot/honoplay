﻿using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public struct TenantDetailModel
    {
        public Guid Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }

        public static Expression<Func<Tenant, TenantDetailModel>> Projection
        {
            get
            {
                return tenant => new TenantDetailModel
                {
                    Id = tenant.Id,
                    Name = tenant.Name,
                    HostName = tenant.HostName,
                    Description = tenant.Description,
                    Logo = tenant.Logo,
                    CreatedBy = tenant.CreatedBy,
                    CreatedAt = tenant.CreatedAt,
                    UpdatedBy = tenant.UpdatedBy,
                    UpdatedAt = tenant.UpdatedAt
                };
            }
        }

        public static TenantDetailModel Create(Tenant customer)
        {
            return Projection.Compile().Invoke(customer);
        }
    }
}