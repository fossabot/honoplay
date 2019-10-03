using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList
{
    public struct TraineeGroupsListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<TraineeGroup, TraineeGroupsListModel>> Projection
        {
            get
            {
                return traineeGroup => new TraineeGroupsListModel
                {
                    Id = traineeGroup.Id,
                    Name = traineeGroup.Name,
                    TenantId = traineeGroup.TenantId,
                    CreatedBy = traineeGroup.CreatedBy,
                    UpdatedBy = traineeGroup.UpdatedBy,
                    UpdatedAt = traineeGroup.UpdatedAt,
                    CreatedAt = traineeGroup.CreatedAt
                };
            }
        }
        public static TraineeGroupsListModel Create(TraineeGroup traineeGroup)
        {
            return Projection.Compile().Invoke(traineeGroup);
        }
    }
}
