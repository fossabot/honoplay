using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            ClassroomTraineeUsers = new HashSet<ClassroomTraineeUser>();
            Sessions = new HashSet<Session>();
        }
        public int Id { get; set; }
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTimeOffset BeginDatetime { get; set; }
        public DateTimeOffset EndDatetime { get; set; }
        public string Location { get; set; }

        public TrainerUser TrainerUser { get; set; }
        public Training Training { get; set; }
        public virtual ICollection<ClassroomTraineeUser> ClassroomTraineeUsers { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

    }
}
