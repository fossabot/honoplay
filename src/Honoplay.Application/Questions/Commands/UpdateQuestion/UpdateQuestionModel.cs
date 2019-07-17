using System;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public struct UpdateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public int Duration { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateQuestionModel(int id, string text, int duration, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
