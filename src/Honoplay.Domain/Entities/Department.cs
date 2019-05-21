﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Department()
        {
            Trainees = new HashSet<Trainee>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public Tenant Tenant { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trainee> Trainees { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}
