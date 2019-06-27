using FluentValidation;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserValidator : AbstractValidator<AuthenticateAdminUserCommand>
    {
        public AuthenticateAdminUserValidator()
        {
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Email).MaximumLength(150).WithMessage("MaxLength");

            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Password).MaximumLength(50);
        }
    }

}
