using FluentValidation;

namespace Honoplay.Application.Sessions.Commands.UpdateSession
{
    public class UpdateSessionValidator : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.GameId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ClassroomId)
                .NotNull()
                .NotEmpty();
        }
    }
}
