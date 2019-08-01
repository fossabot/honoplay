using System;

namespace Honoplay.Application.Trainings.Commands.UpdateTraining
{
    public struct UpdateTrainingModel
    {
        public int Id { get; }
        public int TrainingSeriesId { get; }
        public int TrainingCategoryId { get; }
        public string Name { get; }
        public string Description { get; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }
        public DateTimeOffset BeginDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public UpdateTrainingModel(int id, int trainingSeriesId, int trainingCategoryId, string name, string description, int updatedBy, DateTimeOffset updatedAt, DateTimeOffset beginDateTime, DateTimeOffset endDateTime)
        {
            Id = id;
            TrainingSeriesId = trainingSeriesId;
            TrainingCategoryId = trainingCategoryId;
            Name = name;
            Description = description;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            BeginDateTime = beginDateTime;
            EndDateTime = endDateTime;

        }
    }
}
