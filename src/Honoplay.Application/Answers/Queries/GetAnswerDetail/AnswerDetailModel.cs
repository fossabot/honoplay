using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Answers.Queries.GetAnswerDetail
{
    public struct AnswerDetailModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private static Expression<Func<Answer, AnswerDetailModel>> Projection
        {
            get
            {
                return answer => new AnswerDetailModel
                {
                    Id = answer.Id,
                    UpdatedBy = answer.UpdatedBy,
                    UpdatedAt = answer.UpdatedAt,
                    CreatedBy = answer.CreatedBy,
                    CreatedAt = answer.CreatedAt,
                    Text = answer.Text,
                    QuestionId = answer.QuestionId,
                    OrderBy = answer.OrderBy
                };
            }
        }

        public static AnswerDetailModel Create(Answer answer)
        {
            return Projection.Compile().Invoke(answer);
        }
    }
}
