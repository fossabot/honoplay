using FluentValidation;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.Duration)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty();
        }
    }
}
