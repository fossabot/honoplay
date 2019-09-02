using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Options.Queries.GetOptionsListByQuestionId
{
    public struct OptionsListByQuestionIdModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int? VisibilityOrder { get; set; }
        public bool IsCorrect { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Option, OptionsListByQuestionIdModel>> Projection
        {
            get
            {
                return option => new OptionsListByQuestionIdModel
                {
                    Id = option.Id,
                    Text = option.Text,
                    QuestionId = option.QuestionId,
                    VisibilityOrder = option.VisibilityOrder,
                    CreatedBy = option.CreatedBy,
                    UpdatedBy = option.UpdatedBy,
                    UpdatedAt = option.UpdatedAt,
                    CreatedAt = option.CreatedAt,
                    IsCorrect = option.IsCorrect
                };
            }
        }
        public static OptionsListByQuestionIdModel Create(Option option)
        {
            return Projection.Compile().Invoke(option);
        }
    }
}
