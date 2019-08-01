using System;

namespace Honoplay.Application.Trainings.Commands.CreateTraining
{
    public struct CreateTrainingModel
    {
        public int Id { get; }
        public int TrainingSeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateTrainingModel(int id, int trainingSeriesId, string name, string description, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            TrainingSeriesId = trainingSeriesId;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
