using Honoplay.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public struct UpdateTenantModel
    {
        public Guid Id { get; }
        public DateTimeOffset UpdatedAt { get; }
        public string Name { get; }
        public string Description { get; }
        public string HostName { get; }
        public byte[] Logo { get; }

        public UpdateTenantModel(Guid id, DateTimeOffset updatedAt, string name, string description, string hostName, byte[] logo)
        {
            Id = id;
            UpdatedAt = updatedAt;
            Name = name;
            Description = description;
            HostName = hostName;
            Logo = logo;
        }
    }
}