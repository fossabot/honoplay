using FluentValidation;

namespace Honoplay.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ToQuestion)
                .NotNull()
                .NotEmpty();

        }
    }
}
