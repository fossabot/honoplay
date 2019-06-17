using FluentValidation;

namespace Honoplay.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>,IJavascriptValidator
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Departments).NotEmpty().NotNull();
        }
    }
}
