using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Options.Queries.GetOptionDetail
{
    public struct OptionDetailModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private static Expression<Func<Option, OptionDetailModel>> Projection
        {
            get
            {
                return option => new OptionDetailModel
                {
                    Id = option.Id,
                    UpdatedBy = option.UpdatedBy,
                    UpdatedAt = option.UpdatedAt,
                    CreatedBy = option.CreatedBy,
                    CreatedAt = option.CreatedAt,
                    Text = option.Text,
                    QuestionId = option.QuestionId,
                    OrderBy = option.OrderBy
                };
            }
        }

        public static OptionDetailModel Create(Option option)
        {
            return Projection.Compile().Invoke(option);
        }
    }
}
