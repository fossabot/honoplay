using System;

namespace Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public struct UpdateTrainingSeriesModel
    {
        public int Id { get; }
        public string Name { get; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }

        public UpdateTrainingSeriesModel(int id, string name, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
