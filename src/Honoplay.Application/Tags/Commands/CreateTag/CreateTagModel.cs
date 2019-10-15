using System;

namespace Honoplay.Application.Tags.Commands.CreateTag
{
    public struct CreateTagModel
    {
        public int Id { get; }
        public string Name { get; }
        public bool ToQuestion { get; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateTagModel(int id, string name, bool toQuestion, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            ToQuestion = toQuestion;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
