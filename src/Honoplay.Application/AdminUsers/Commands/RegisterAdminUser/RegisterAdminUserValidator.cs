using FluentValidation;
using Honoplay.FluentValidatorJavascript;

namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserValidator : AbstractValidator<RegisterAdminUserCommand>
    {
        public RegisterAdminUserValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage(MessageCodes.NotNullValidator);
            RuleFor(x => x.Email).NotEmpty().WithMessage(MessageCodes.NotEmptyValidator);
            RuleFor(x => x.Email).MaximumLength(150).WithMessage(MessageCodes.MaximumLengthValidator);
            RuleFor(x => x.Email).EmailAddress().WithMessage(MessageCodes.EmailValidator);

            RuleFor(x => x.Password).NotNull().WithMessage(MessageCodes.NotNullValidator);
            RuleFor(x => x.Password).NotEmpty().WithMessage(MessageCodes.NotEmptyValidator);
            RuleFor(x => x.Password).MinimumLength(6).WithMessage(MessageCodes.MinimumLengthValidator);
            RuleFor(x => x.Password).MaximumLength(50).WithMessage(MessageCodes.MaximumLengthValidator);

            RuleFor(x => x.Name).NotNull().WithMessage(MessageCodes.NotNullValidator);
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageCodes.NotEmptyValidator);
            RuleFor(x => x.Name).MaximumLength(50).WithMessage(MessageCodes.MaximumLengthValidator);

            RuleFor(x => x.Surname).NotNull().WithMessage(MessageCodes.NotNullValidator);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(MessageCodes.NotEmptyValidator);
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage(MessageCodes.MaximumLengthValidator);
        }
    }

}
