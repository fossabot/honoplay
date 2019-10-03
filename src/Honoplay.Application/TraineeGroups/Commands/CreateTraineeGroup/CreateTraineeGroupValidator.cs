using FluentValidation;

namespace Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup
{
    public class CreateTraineeGroupValidator : AbstractValidator<CreateTraineeGroupCommand>
    {
        public CreateTraineeGroupValidator()
        {
            RuleForEach(x => x.CreateTraineeGroupCommandModels).SetValidator(new InlineValidator<CreateTraineeGroupCommandModel> {
                orderValidator => orderValidator.RuleFor(x => x.Name).NotNull().NotEmpty()
            });

        }

    }
}