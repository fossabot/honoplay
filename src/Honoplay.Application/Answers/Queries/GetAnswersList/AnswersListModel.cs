using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Answers.Queries.GetAnswersList
{
    public struct AnswersListModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Answer, AnswersListModel>> Projection
        {
            get
            {
                return answer => new AnswersListModel
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    QuestionId = answer.QuestionId,
                    OrderBy = answer.OrderBy,
                    CreatedBy = answer.CreatedBy,
                    UpdatedBy = answer.UpdatedBy,
                    UpdatedAt = answer.UpdatedAt,
                    CreatedAt = answer.CreatedAt
                };
            }
        }
        public static AnswersListModel Create(Answer answer)
        {
            return Projection.Compile().Invoke(answer);
        }
    }
}
