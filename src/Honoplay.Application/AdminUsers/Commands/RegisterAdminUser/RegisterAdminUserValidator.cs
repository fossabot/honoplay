using FluentValidation;


namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserValidator : AbstractValidator<RegisterAdminUserCommand>
    {
        public RegisterAdminUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(150).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Surname).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
