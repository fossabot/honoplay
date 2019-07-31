using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class TrainingSeries : BaseEntity
    {
        public TrainingSeries()
        {
            Trainings = new HashSet<Training>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}
