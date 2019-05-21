using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Honoplay.Application.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerValidator : AbstractValidator<CreateTrainerCommand>
    {
        public CreateTrainerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProfessionId)
                .NotEmpty()
                .NotNull();
        }
    }
}
