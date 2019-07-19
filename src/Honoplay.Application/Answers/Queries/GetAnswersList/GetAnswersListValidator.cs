﻿using FluentValidation;

namespace Honoplay.Application.Answers.Queries.GetAnswersList
{
    public class GetAnswersListValidator : AbstractValidator<GetAnswersListQueryModel>
    {
        public GetAnswersListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
