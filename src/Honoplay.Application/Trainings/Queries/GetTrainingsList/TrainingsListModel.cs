using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsList
{
    public struct TrainingsListModel
    {
        public int Id { get; set; }
        public int TrainingSeriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Training, TrainingsListModel>> Projection
        {
            get
            {
                return training => new TrainingsListModel
                {
                    Id = training.Id,
                    Name = training.Name,
                    Description = training.Description,
                    TrainingSeriesId = training.TrainingSeriesId,
                    CreatedBy = training.CreatedBy,
                    UpdatedBy = training.UpdatedBy,
                    UpdatedAt = training.UpdatedAt,
                    CreatedAt = training.CreatedAt
                };
            }
        }
        public static TrainingsListModel Create(Training training)
        {
            return Projection.Compile().Invoke(training);
        }
    }
}
