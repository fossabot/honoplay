using FluentValidation;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserValidator : AbstractValidator<AuthenticateAdminUserCommand>
    {
        public AuthenticateAdminUserValidator()
        {
            RuleFor(x => x.Username).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Password).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
