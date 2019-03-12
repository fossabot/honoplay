using FluentValidation;


namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserValidator : AbstractValidator<AuthenticateAdminUserCommand>
    {
        public AuthenticateAdminUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(150).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
