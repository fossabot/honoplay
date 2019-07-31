using FluentValidation;

namespace Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries
{
    public class CreateTrainingSeriesValidator : AbstractValidator<CreateTrainingSeriesCommand>
    {
        public CreateTrainingSeriesValidator()
        {
            RuleForEach(x => x.CreateTrainingSeriesModels)
                .SetValidator(new InlineValidator<CreateTrainingSeriesCommandModel> {
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.Name)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(50)
                });
        }
    }
}
