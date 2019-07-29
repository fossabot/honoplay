using System;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public struct CreateOptionModel
    {
        public int Id { get; }
        public int QuestionId { get; }
        public string Text { get; }
        public int? VisibilityOrder { get; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }
        public bool IsCorrect { get; }

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
