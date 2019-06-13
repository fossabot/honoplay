using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Honoplay.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public Tenant()
        {
            //Default values for non nullable refs.
            Name = HostName = "";

            TenantAdminUsers = new HashSet<TenantAdminUser>();
            Departments = new HashSet<Department>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }
        [JsonIgnore]

        public virtual ICollection<TenantAdminUser> TenantAdminUsers { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}