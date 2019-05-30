using FluentValidation;

namespace Honoplay.Application.Tenants.Commands.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Departments).NotEmpty().NotNull();
        }
    }
}
