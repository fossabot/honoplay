using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList
{
    public struct QuestionDifficultiesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<QuestionDifficulty, QuestionDifficultiesListModel>> Projection
        {
            get
            {
                return questionDifficulty => new QuestionDifficultiesListModel
                {
                    Id = questionDifficulty.Id,
                    Name = questionDifficulty.Name
                };
            }
        }
        public static QuestionDifficultiesListModel Create(QuestionDifficulty questionDifficulty)
        {
            return Projection.Compile().Invoke(questionDifficulty);
        }
    }
}
