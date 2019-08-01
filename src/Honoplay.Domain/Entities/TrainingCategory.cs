using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class TrainingCategory : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Training> Trainings { get; set; }
    }
}
