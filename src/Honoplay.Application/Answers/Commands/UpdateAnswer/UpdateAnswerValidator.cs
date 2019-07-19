using FluentValidation;

namespace Honoplay.Application.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerValidator : AbstractValidator<UpdateAnswerCommand>
    {
        public UpdateAnswerValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

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
