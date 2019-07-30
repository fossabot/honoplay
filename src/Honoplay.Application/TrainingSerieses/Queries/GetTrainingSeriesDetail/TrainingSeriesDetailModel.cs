using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesDetail
{
    public struct TrainingSeriesDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private static Expression<Func<TrainingSeries, TrainingSeriesDetailModel>> Projection
        {
            get
            {
                return option => new TrainingSeriesDetailModel
                {
                    Id = option.Id,
                    UpdatedBy = option.UpdatedBy,
                    UpdatedAt = option.UpdatedAt,
                    CreatedBy = option.CreatedBy,
                    CreatedAt = option.CreatedAt,
                    Name = option.Name
                };
            }
        }

        public static TrainingSeriesDetailModel Create(TrainingSeries trainingSeries)
        {
            return Projection.Compile().Invoke(trainingSeries);
        }
    }
}
