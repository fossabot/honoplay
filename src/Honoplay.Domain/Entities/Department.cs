using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public Tenant Tenant { get; set; }
    }
}
