using FluentValidation;
using Honoplay.FluentValidatorJavascript;

namespace Honoplay.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Departments)
                .ForEach(x =>
                    x.NotNull().WithMessage(MessageCodes.NotNullValidator)
                        .NotNull().WithMessage(MessageCodes.NotEmptyValidator))
                .NotNull().WithMessage(MessageCodes.NotNullValidator)
                .NotEmpty().WithMessage(MessageCodes.NotEmptyValidator);
        }
    }
}
