using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            ClassroomTrainees = new HashSet<ClassroomTrainee>();
            Sessions = new HashSet<Session>();
        }
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Trainer Trainer { get; set; }
        public Training Training { get; set; }
        public virtual ICollection<ClassroomTrainee> ClassroomTrainees { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

    }
}
