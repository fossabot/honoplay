using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList
{
    public struct QuestionTypesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<QuestionType, QuestionTypesListModel>> Projection
        {
            get
            {
                return questionType => new QuestionTypesListModel
                {
                    Id = questionType.Id,
                    Name = questionType.Name
                };
            }
        }
        public static QuestionTypesListModel Create(QuestionType questionType)
        {
            return Projection.Compile().Invoke(questionType);
        }
    }
}
