using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class TraineeGroup : BaseEntity
    {
        public TraineeGroup()
        {
            TraineeUsers = new HashSet<TraineeUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public Tenant Tenant { get; set; }
        [JsonIgnore]
        public virtual ICollection<TraineeUser> TraineeUsers { get; set; }
    }
}
