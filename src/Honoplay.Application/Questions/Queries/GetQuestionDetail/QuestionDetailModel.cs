using Honoplay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public struct QuestionDetailModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? DifficultyId { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public Guid? ContentFileId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public static Expression<Func<Question, QuestionDetailModel>> Projection => question => new QuestionDetailModel
        {
            Id = question.Id,
            DifficultyId = question.QuestionDifficultyId,
            ContentFileId = question.ContentFileId,
            CategoryId = question.QuestionCategoryId,
            TypeId = question.QuestionTypeId,
            Tags = question.QuestionTags.Select(x => x.Tag).ToList(),
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
