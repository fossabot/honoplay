using FluentValidation;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserValidator : AbstractValidator<AuthenticateAdminUserCommand>
    {
        public AuthenticateAdminUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(50).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
