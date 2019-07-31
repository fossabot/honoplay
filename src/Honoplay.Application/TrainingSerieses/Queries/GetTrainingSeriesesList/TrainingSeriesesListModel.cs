using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public struct TrainingSeriesesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<TrainingSeries, TrainingSeriesesListModel>> Projection
        {
            get
            {
                return trainingSeries => new TrainingSeriesesListModel
                {
                    Id = trainingSeries.Id,
                    Name = trainingSeries.Name,
                    CreatedBy = trainingSeries.CreatedBy,
                    UpdatedBy = trainingSeries.UpdatedBy,
                    UpdatedAt = trainingSeries.UpdatedAt,
                    CreatedAt = trainingSeries.CreatedAt
                };
            }
        }
        public static TrainingSeriesesListModel Create(TrainingSeries trainingSeries)
        {
            return Projection.Compile().Invoke(trainingSeries);
        }
    }
}
