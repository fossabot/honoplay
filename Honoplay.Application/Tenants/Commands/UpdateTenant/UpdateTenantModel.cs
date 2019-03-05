#nullable enable

using System;

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public readonly struct UpdateTenantModel
    {
        public Guid Id { get; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }
        public string? Name { get; }
        public string? Description { get; }
        public string? HostName { get; }
        public byte[]? Logo { get; }

        public UpdateTenantModel(Guid id, int updatedBy, DateTimeOffset updatedAt, string name, string description, string hostName, byte[] logo)
        {
            Id = id;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            Name = name;
            Description = description;
            HostName = hostName;
            Logo = logo;
        }
    }
}