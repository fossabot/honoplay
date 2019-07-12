using FluentValidation;
using Honoplay.FluentValidatorJavascript;

namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserValidator : AbstractValidator<RegisterAdminUserCommand>
    {
        public RegisterAdminUserValidator()
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

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }

}
