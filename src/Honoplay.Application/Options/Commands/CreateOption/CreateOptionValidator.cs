using FluentValidation;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleForEach(x => x.CreateOptionModels).SetValidator(new InlineValidator<CreateOptionCommandModel> {
                orderValidator => orderValidator.RuleFor(x => x.Text).NotNull().NotEmpty(),
                orderValidator => orderValidator.RuleFor(x => x.QuestionId).NotNull().NotEmpty()
            });

        }

    }
}
