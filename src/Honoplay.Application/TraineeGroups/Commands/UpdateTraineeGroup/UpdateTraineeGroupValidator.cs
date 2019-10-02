using FluentValidation;

namespace Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupValidator : AbstractValidator<UpdateTraineeGroupCommand>
    {
        public UpdateTraineeGroupValidator()
        {
            RuleForEach(x => x.UpdateTraineeGroupCommandModels).SetValidator(new InlineValidator<UpdateTraineeGroupCommandModel> {
                orderValidator => orderValidator.RuleFor(x => x.Name).NotNull().NotEmpty()
            });

        }

    }
}