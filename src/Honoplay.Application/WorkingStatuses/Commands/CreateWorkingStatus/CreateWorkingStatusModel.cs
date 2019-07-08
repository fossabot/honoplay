using System;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public struct CreateWorkingStatusModel
    {
        public string Name { get; set; }
        public int CreatedBy { get; set; }

        public CreateWorkingStatusModel(string name, int createdBy)
        {
            Name = name;
            CreatedBy = createdBy;
        }
    }
}
