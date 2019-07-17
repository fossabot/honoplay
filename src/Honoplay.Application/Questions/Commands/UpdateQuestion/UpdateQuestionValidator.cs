using FluentValidation;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Duration)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty();
        }
    }
}
