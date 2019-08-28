using FluentValidation;

namespace Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser
{
    public class AuthenticateTraineeUserValidator : AbstractValidator<AuthenticateTraineeUserCommand>
    {
        public AuthenticateTraineeUserValidator()
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
