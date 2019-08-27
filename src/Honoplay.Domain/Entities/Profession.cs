using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Profession : BaseEntity
    {
        public Profession()
        {
            TrainerUsers = new HashSet<TrainerUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }

        [JsonIgnore]
        public virtual ICollection<TrainerUser> TrainerUsers { get; set; }
        public Tenant Tenant { get; set; }
    }
}
