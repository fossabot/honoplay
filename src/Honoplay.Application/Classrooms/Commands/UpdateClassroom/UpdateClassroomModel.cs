using System;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public struct UpdateClassroomModel
    {
        public int Id { get; }
        public int TrainerUserId { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }

        public UpdateClassroomModel(int id, int trainerUserId, int trainingId, string name, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            TrainerUserId = trainerUserId;
            TrainingId = trainingId;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
