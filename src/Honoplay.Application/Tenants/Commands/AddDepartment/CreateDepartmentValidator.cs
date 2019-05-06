using FluentValidation;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Departments).NotEmpty().NotNull();
            RuleFor(x => x.TenantId).NotEmpty().NotNull();
        }
    }
}
