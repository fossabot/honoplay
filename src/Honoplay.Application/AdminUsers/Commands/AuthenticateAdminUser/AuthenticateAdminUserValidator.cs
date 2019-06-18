using FluentValidation;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserValidator : AbstractValidator<AuthenticateAdminUserCommand>
    {
        public AuthenticateAdminUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(150).WithMessage("MaxLength");
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
