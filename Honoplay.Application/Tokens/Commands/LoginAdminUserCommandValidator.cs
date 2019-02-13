using FluentValidation;
#nullable enable

namespace Honoplay.Application.Tokens.Commands
{
    public class LoginAdminUserValidator : AbstractValidator<LoginAdminUserCommand>
    {
        public LoginAdminUserValidator()
        {
            RuleFor(x => x.Username).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Password).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
