using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public struct QuestionDifficultyDetailModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public static Expression<Func<QuestionDifficulty, QuestionDifficultyDetailModel>> Projection
        {
            get
            {
                return questionDifficulty => new QuestionDifficultyDetailModel
                {
                    Id = questionDifficulty.Id,
                    Name = questionDifficulty.Name
                };
            }
        }
        public static QuestionDifficultyDetailModel Create(QuestionDifficulty questionDifficulty)
        {
            return Projection.Compile().Invoke(questionDifficulty);
        }
    }
}
