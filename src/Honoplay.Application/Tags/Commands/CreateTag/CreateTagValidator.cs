using FluentValidation;

namespace Honoplay.Application.Tags.Commands.CreateTag
{
    public class CreateTagValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagValidator()
        {
            RuleForEach(x => x.CreateTagModels).SetValidator(new InlineValidator<CreateTagCommandModel> {
                orderValidator => orderValidator.RuleFor(x => x.Name).NotNull().NotEmpty(),
                orderValidator => orderValidator.RuleFor(x => x.ToQuestion).NotNull().NotEmpty()
            });

        }

    }
}
