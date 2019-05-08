using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Honoplay.Domain.Entities
{
    public class WorkingStatus
    {
        public WorkingStatus()
        {
            Trainees = new HashSet<Trainee>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]

        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
