using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public struct QuestionTypeDetailModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public static Expression<Func<QuestionType, QuestionTypeDetailModel>> Projection
        {
            get
            {
                return questionType => new QuestionTypeDetailModel
                {
                    Id = questionType.Id,
                    Name = questionType.Name
                };
            }
        }
        public static QuestionTypeDetailModel Create(QuestionType questionType)
        {
            return Projection.Compile().Invoke(questionType);
        }
    }
}
