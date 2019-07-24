using System;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public struct CreateOptionModel
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        public int? VisibilityOrder { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public bool IsCorrect { get; set; }

        public CreateOptionModel(int id, string text, int? visibilityOrder, int questionId, int createdBy, DateTimeOffset createdAt, bool isCorrect)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            VisibilityOrder = visibilityOrder;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            IsCorrect = isCorrect;
        }
    }
}
