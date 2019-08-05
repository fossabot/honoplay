using FluentValidation;

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
                        .RuleFor(x => x.TrainerId)
                        .NotNull()
                        .NotEmpty(),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.TrainingId)
                        .NotNull()
                        .NotEmpty()
                });
        }
    }
}
