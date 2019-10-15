using System;

namespace Honoplay.Application.Tags.Commands.UpdateTag
{
    public struct UpdateTagModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int UpdatedBy { get; private set; }
        public bool ToQuestion { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateTagModel(int id, string name, bool toQuestion, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            ToQuestion = toQuestion;
        }
    }
}
