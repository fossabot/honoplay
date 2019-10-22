using System;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public struct UpdateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionDifficultyId { get; set; }
        public int? QuestionCategoryId { get; set; }
        public Guid? ContentFileId { get; set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateQuestionModel(int id, string text, int duration, int? questionTypeId, int? questionDifficultyId, int? questionCategoryId, Guid? contentFileId, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            QuestionTypeId = questionTypeId;
            QuestionDifficultyId = questionDifficultyId;
            QuestionCategoryId = questionCategoryId;
            ContentFileId = contentFileId;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
