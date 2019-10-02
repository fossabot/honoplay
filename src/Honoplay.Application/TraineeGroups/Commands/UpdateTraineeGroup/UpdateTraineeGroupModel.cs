using System;

namespace Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup
{
    public struct UpdateTraineeGroupModel
    {
        public int Id { get; }
        public string Name { get;}
        public int? UpdatedBy { get; }
        public DateTimeOffset? UpdatedAt { get; }

        public UpdateTraineeGroupModel(int id, string name, int? updatedBy, DateTimeOffset? updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
