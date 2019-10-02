using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList;
using Honoplay.Domain.Entities;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public struct TraineeGroupDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<TraineeGroup, TraineeGroupDetailModel>> Projection => traineeGroup => new TraineeGroupDetailModel
        {
            Id = traineeGroup.Id,
            Name = traineeGroup.Name,
            TenantId = traineeGroup.TenantId,
            CreatedBy = traineeGroup.CreatedBy,
            UpdatedBy = traineeGroup.UpdatedBy,
            UpdatedAt = traineeGroup.UpdatedAt,
            CreatedAt = traineeGroup.CreatedAt
        };

        public static TraineeGroupDetailModel Create(TraineeGroup traineeGroup) => Projection.Compile().Invoke(traineeGroup);
    }
}
