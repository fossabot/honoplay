using Honoplay.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public struct UpdateTenantModel
    {
        public Guid Id { get; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }
        public string Name { get; }
        public string Description { get; }
        public string HostName { get; }
        public byte[] Logo { get; }
        public ICollection<Department> Departments { get; set; }

        public UpdateTenantModel(Guid id, int updatedBy, DateTimeOffset updatedAt, string name, string description, string hostName, byte[] logo, ICollection<Department> departments)
        {
            Id = id;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            Name = name;
            Description = description;
            HostName = hostName;
            Logo = logo;
            Departments = departments;
        }
    }
}