using System;

namespace Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup
{
    public struct CreateTraineeGroupModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateTraineeGroupModel(int id, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
