namespace Honoplay.Domain.Entities
{
    public class ClassroomTrainee
    {
        public int ClassroomId { get; set; }
        public int TraineeId { get; set; }

        public Classroom Classroom { get; set; }
        public Trainee Trainee { get; set; }
    }
}
