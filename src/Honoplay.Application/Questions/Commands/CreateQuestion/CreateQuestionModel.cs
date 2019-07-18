using System;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public struct CreateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public int Duration { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateQuestionModel(int id, string text, int duration, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
