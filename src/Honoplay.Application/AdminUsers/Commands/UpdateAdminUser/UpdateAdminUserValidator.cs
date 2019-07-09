using FluentValidation;

namespace Honoplay.Application.AdminUsers.Commands.UpdateAdminUser
{
    public class UpdateAdminUserValidator : AbstractValidator<UpdateAdminUserCommand>
    {
        public UpdateAdminUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(150).NotNull().EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Surname).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
