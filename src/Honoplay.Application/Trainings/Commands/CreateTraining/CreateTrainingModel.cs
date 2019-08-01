using System;

namespace Honoplay.Application.Trainings.Commands.CreateTraining
{
    public struct CreateTrainingModel
    {
        public int Id { get; }
        public int TrainingSeriesId { get; set; }
        public int TrainingCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset BeginDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public CreateTrainingModel(int id, int trainingSeriesId, int trainingCategoryId, string name, string description, int createdBy, DateTimeOffset createdAt, DateTimeOffset beginDateTime, DateTimeOffset endDateTime)
        {
            Id = id;
            TrainingSeriesId = trainingSeriesId;
            TrainingCategoryId = trainingCategoryId;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            BeginDateTime = beginDateTime;
            EndDateTime = endDateTime;
        }
    }
}
