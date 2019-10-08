using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public struct QuestionDetailModel
    {
        public int Id { get; private set; }
        public Guid TenantId { get; private set; }
        public string Text { get; private set; }
        public int Duration { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<Question, QuestionDetailModel>> Projection => question => new QuestionDetailModel
        {
            Id = question.Id,
            TenantId = question.TenantId,
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
