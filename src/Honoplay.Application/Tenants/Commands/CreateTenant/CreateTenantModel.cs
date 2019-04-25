using Honoplay.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public struct CreateTenantModel
    {
        public Guid Id { get; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Name { get; }
        public string Description { get; }
        public string HostName { get; }
        public byte[] Logo { get; }
        public ICollection<Department> Departments { get; set; }


        public CreateTenantModel(Guid id, int createdBy, DateTimeOffset createdAt, string name, string description, string hostName, byte[] logo, ICollection<Department> departments)
        {
            Id = id;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            Name = name;
            Description = description;
            HostName = hostName;
            Logo = logo;
            Departments = departments; 
        }
    }
}