using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class WorkingStatus : BaseEntity
    {
        public WorkingStatus()
        {
            TraineeUsers = new HashSet<TraineeUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }

        [JsonIgnore]
        public virtual ICollection<TraineeUser> TraineeUsers { get; set; }
        public Tenant Tenant { get; set; }
    }
}
