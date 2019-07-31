using FluentValidation;

namespace Honoplay.Application.Trainings.Commands.CreateTraining
{
    public class CreateTrainingValidator : AbstractValidator<CreateTrainingCommand>
    {
        public CreateTrainingValidator()
        {
            RuleForEach(x => x.CreateTrainingModels)
                .SetValidator(new InlineValidator<CreateTrainingCommandModel> {
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.Name)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(50),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.TrainingSeriesId)
                        .NotNull()
                        .NotEmpty(),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.Description)
                        .MaximumLength(500)
                });
        }
    }
}
