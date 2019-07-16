using Newtonsoft.Json;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Profession : BaseEntity
    {
        public Profession()
        {
            Trainers = new HashSet<Trainer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}
