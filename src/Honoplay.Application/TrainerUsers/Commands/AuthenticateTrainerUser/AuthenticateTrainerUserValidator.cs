using FluentValidation;

namespace Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser
{
    public class AuthenticateTrainerUserValidator : AbstractValidator<AuthenticateTrainerUserCommand>
    {
        public AuthenticateTrainerUserValidator()
        {

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);
        }
    }

}
