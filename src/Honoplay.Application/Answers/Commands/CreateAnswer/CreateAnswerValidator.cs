using FluentValidation;

namespace Honoplay.Application.Answers.Commands.CreateAnswer
{
    public class CreateAnswerValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerValidator()
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
