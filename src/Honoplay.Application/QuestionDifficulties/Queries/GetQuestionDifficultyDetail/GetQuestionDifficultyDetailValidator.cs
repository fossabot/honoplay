﻿using FluentValidation;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public class GetQuestionDifficultyDetailValidator : AbstractValidator<GetQuestionDifficultyDetailQuery>
    {
        public GetQuestionDifficultyDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
