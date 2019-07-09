using System;

namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public struct CreateTenantModel
    {
        public Guid Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Name { get; }
        public string Description { get; }
        public string HostName { get; }
        public byte[] Logo { get; }

        public CreateTenantModel(Guid id, DateTimeOffset createdAt, string name, string description, string hostName, byte[] logo)
        {
            Id = id;
            CreatedAt = createdAt;
            Name = name;
            Description = description;
            HostName = hostName;
            Logo = logo;
        }
    }
}