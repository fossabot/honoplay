using System;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public struct UpdateWorkingStatusModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Guid TenantId { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateWorkingStatusModel(int id, string name, Guid tenantId, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
