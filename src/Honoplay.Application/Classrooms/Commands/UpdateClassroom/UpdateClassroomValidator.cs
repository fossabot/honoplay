using FluentValidation;
using System;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomValidator : AbstractValidator<UpdateClassroomCommand>
    {
        public UpdateClassroomValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.TrainerUserId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TrainingId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.BeginDatetime)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTimeOffset.Now);

            RuleFor(x => x.EndDatetime)
                .NotNull()
                .NotEmpty()
                .GreaterThan(DateTimeOffset.Now)
                .GreaterThan(x => x.BeginDatetime);
        }
    }
}
