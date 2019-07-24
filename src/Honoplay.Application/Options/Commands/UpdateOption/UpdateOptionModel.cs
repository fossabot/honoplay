using System;

namespace Honoplay.Application.Options.Commands.UpdateOption
{
    public struct UpdateOptionModel
    {
        public int Id { get; private set; }
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        public int OrderBy { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateOptionModel(int id, string text, int orderBy, int questionId, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            OrderBy = orderBy;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
