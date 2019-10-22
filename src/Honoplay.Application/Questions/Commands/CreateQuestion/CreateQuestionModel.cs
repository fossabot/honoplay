using System;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public struct CreateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionDifficultyId { get; set; }
        public int? QuestionCategoryId { get; set; }
        public Guid? ContentFileId { get; set; }

        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateQuestionModel(int id, string text, int duration, int? questionTypeId, int? questionDifficultyId, int? questionCategoryId, Guid? contentFileId, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            QuestionTypeId = questionTypeId;
            QuestionDifficultyId = questionDifficultyId;
            QuestionCategoryId = questionCategoryId;
            ContentFileId = contentFileId;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
