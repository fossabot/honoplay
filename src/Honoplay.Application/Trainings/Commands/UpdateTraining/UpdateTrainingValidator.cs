using FluentValidation;

namespace Honoplay.Application.Trainings.Commands.UpdateTraining
{
    public class UpdateTrainingValidator : AbstractValidator<UpdateTrainingCommand>
    {
        public UpdateTrainingValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.TrainingSeriesId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
