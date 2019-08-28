using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Department()
        {
            TraineeUsers = new HashSet<TraineeUser>();
            TrainerUsers = new HashSet<TrainerUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public Tenant Tenant { get; set; }
        [JsonIgnore]
        public virtual ICollection<TraineeUser> TraineeUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<TrainerUser> TrainerUsers { get; set; }
    }
}
