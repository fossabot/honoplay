namespace Honoplay.Domain.Entities
{
    public class ClassroomTraineeUser
    {
        public int ClassroomId { get; set; }
        public int TraineeUserId { get; set; }

        public Classroom Classroom { get; set; }
        public TraineeUser TraineeUser { get; set; }
    }
}
