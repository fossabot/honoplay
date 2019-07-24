using FluentValidation;

namespace Honoplay.Application.Options.Commands.UpdateOption
{
    public class UpdateOptionValidator : AbstractValidator<UpdateOptionCommand>
    {
        public UpdateOptionValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.VisibilityOrder)
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
