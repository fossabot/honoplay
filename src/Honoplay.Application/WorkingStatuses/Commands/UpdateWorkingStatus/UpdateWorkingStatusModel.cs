using System;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public struct UpdateWorkingStatusModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateWorkingStatusModel(int id, string name, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
