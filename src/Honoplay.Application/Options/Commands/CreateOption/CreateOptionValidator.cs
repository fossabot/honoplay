using FluentValidation;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleFor(x => x.OrderBy)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.QuestionId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty();

        }
    }
}
