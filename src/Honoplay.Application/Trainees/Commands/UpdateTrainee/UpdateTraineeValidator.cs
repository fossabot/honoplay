using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class UpdateTraineeValidator : AbstractValidator<CreateTraineeCommand>
    {
        public UpdateTraineeValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(150).MinimumLength(1);
        }
    }
}
