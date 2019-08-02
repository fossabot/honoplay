using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            Trainees = new HashSet<Trainee>();
            Levels = new HashSet<Level>();
        }
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }

        public Trainer Trainer { get; set; }
        public Training Training { get; set; }
        public virtual ICollection<Trainee> Trainees { get; set; }
        public virtual ICollection<Level> Levels { get; set; }

    }
}
