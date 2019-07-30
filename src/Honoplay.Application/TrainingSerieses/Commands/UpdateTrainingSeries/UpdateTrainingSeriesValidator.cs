using FluentValidation;

namespace Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public class UpdateTrainingSeriesValidator : AbstractValidator<UpdateTrainingSeriesCommand>
    {
        public UpdateTrainingSeriesValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
