using System;

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public struct CreateClassroomModel
    {
        public int Id { get; }
        public int TrainerId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateClassroomModel(int id, int trainerId, int trainingId, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            TrainerId = trainerId;
            TrainingId = trainingId;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
