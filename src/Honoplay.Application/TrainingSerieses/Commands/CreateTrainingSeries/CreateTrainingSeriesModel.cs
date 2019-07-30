using System;

namespace Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries
{
    public struct CreateTrainingSeriesModel
    {
        public int Id { get; }
        public string Name { get; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateTrainingSeriesModel(int id, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
