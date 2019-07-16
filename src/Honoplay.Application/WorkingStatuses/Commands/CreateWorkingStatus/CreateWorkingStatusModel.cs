using System;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public struct CreateWorkingStatusModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateWorkingStatusModel(int id, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
