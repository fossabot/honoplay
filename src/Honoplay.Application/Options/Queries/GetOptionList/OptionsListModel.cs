using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Options.Queries.GetOptionsList
{
    public struct OptionsListModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Option, OptionsListModel>> Projection
        {
            get
            {
                return option => new OptionsListModel
                {
                    Id = option.Id,
                    Text = option.Text,
                    QuestionId = option.QuestionId,
                    OrderBy = option.OrderBy,
                    CreatedBy = option.CreatedBy,
                    UpdatedBy = option.UpdatedBy,
                    UpdatedAt = option.UpdatedAt,
                    CreatedAt = option.CreatedAt
                };
            }
        }
        public static OptionsListModel Create(Option option)
        {
            return Projection.Compile().Invoke(option);
        }
    }
}
