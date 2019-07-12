using System;
using System.Linq.Expressions;
using Honoplay.Domain.Entities;

namespace Honoplay.Application.WorkingStatuses.Queries
{
    public struct WorkingStatusesListModel
    {
        public int Id { get; private set; }
        public Guid TenantId { get; private set; }
        public string Name { get; private set; }

        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<WorkingStatus, WorkingStatusesListModel>> Projection
        {
            get
            {
                return workingStatus => new WorkingStatusesListModel
                {
                    Id = workingStatus.Id,
                    Name = workingStatus.Name,
                    TenantId = workingStatus.TenantId,
                    CreatedBy = workingStatus.CreatedBy,
                    CreatedAt = workingStatus.CreatedAt,
                    UpdatedBy = workingStatus.UpdatedBy,
                    UpdatedAt = workingStatus.UpdatedAt
                };
            }
        }

        public static WorkingStatusesListModel Create(WorkingStatus workingStatus)
        {
            return Projection.Compile().Invoke(workingStatus);
        }
    }
}
