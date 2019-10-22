using FluentValidation;
using System;

namespace Honoplay.Application.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomValidator : AbstractValidator<CreateClassroomCommand>
    {
        public CreateClassroomValidator()
        {
            RuleForEach(x => x.CreateClassroomModels)
                .SetValidator(new InlineValidator<CreateClassroomCommandModel> {
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.Name)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(50),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.TrainerUserId)
                        .NotNull()
                        .NotEmpty(),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.TrainingId)
                        .NotNull()
                        .NotEmpty(),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.BeginDatetime)
                        .NotNull()
                        .NotEmpty()
                        .GreaterThanOrEqualTo(DateTimeOffset.Now),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.EndDatetime)
                        .NotNull()
                        .NotEmpty()
                        .GreaterThan(DateTimeOffset.Now)
                        .GreaterThan(x => x.BeginDatetime)
                });
        }
    }
}
