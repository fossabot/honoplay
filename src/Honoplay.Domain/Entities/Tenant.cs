using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
            TraineeGroups = new HashSet<TraineeGroup>();
            WorkingStatuses = new HashSet<WorkingStatus>();
            Questions = new HashSet<Question>();
            TrainingSerieses = new HashSet<TrainingSeries>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }

        [JsonIgnore]
        public virtual ICollection<TenantAdminUser> TenantAdminUsers { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<TraineeGroup> TraineeGroups { get; set; }
        public virtual ICollection<Profession> Professions { get; set; }
        public virtual ICollection<WorkingStatus> WorkingStatuses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TrainingSeries> TrainingSerieses { get; set; }
    }
}