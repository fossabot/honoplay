using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public struct QuestionDetailModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionDifficultyId { get; set; }
        public int? QuestionCategoryId { get; set; }
        public Guid? ContentFileId { get; set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public static Expression<Func<Question, QuestionDetailModel>> Projection => question => new QuestionDetailModel
        {
            Id = question.Id,
            QuestionDifficultyId = question.QuestionDifficultyId,
            ContentFileId = question.ContentFileId,
            QuestionCategoryId = question.QuestionCategoryId,
            QuestionTypeId = question.QuestionTypeId,
            CreatedBy = question.CreatedBy,
            UpdatedBy = question.UpdatedBy,
            UpdatedAt = question.UpdatedAt,
            CreatedAt = question.CreatedAt,
            Text = question.Text,
            Duration = question.Duration
        };

        public static QuestionDetailModel Create(Question question) => Projection.Compile().Invoke(question);
    }
}
