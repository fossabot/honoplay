using System;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public struct UpdateWorkingStatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public int UpdatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public UpdateWorkingStatusModel(int id, string name, string hostName, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            HostName = hostName;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
