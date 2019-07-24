using System;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public struct CreateOptionModel
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        public int OrderBy { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateOptionModel(int id, string text, int orderBy, int questionId, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            OrderBy = orderBy;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
