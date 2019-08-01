using System;

namespace Honoplay.Application.Trainings.Commands.UpdateTraining
{
    public struct UpdateTrainingModel
    {
        public int Id { get; }
        public int TrainingSeriesId { get; }
        public string Name { get; }
        public string Description { get; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }

        public UpdateTrainingModel(int id, int trainingSeriesId, string name, string description, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            TrainingSeriesId = trainingSeriesId;
            Name = name;
            Description = description;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
