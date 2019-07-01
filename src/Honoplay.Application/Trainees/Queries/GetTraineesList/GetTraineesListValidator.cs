﻿using FluentValidation;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListValidator : AbstractValidator<GetTraineesListQueryModel>
    {
        public GetTraineesListValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .NotNull()
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}