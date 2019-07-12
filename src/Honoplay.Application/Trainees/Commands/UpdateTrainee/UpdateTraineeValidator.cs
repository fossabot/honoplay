﻿using FluentValidation;

namespace Honoplay.Application.Trainees.Commands.UpdateTrainee
{
    public class UpdateTraineeValidator : AbstractValidator<UpdateTraineeCommand>
    {
        public UpdateTraineeValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.NationalIdentityNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);

            RuleFor(x => x.WorkingStatusId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Gender)
                .NotNull()
                .GreaterThan(-1)
                .LessThan(2);
        }
    }
}
