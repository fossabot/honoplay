using System;

namespace Honoplay.Application.Options.Commands.UpdateOption
{
    public struct UpdateOptionModel
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        public int? VisibilityOrder { get; private set; }
        public int UpdatedBy { get; private set; }
        public bool IsCorrect { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateOptionModel(int id, string text, int? visibilityOrder, int questionId, int updatedBy, bool isCorrect, DateTimeOffset updatedAt)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            VisibilityOrder = visibilityOrder;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            IsCorrect = isCorrect;
        }
    }
}
