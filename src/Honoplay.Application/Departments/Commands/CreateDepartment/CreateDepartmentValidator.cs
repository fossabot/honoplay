using FluentValidation;

namespace Honoplay.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Departments)
                .ForEach(x => x.NotNull().NotEmpty())
                .NotNull()
                .NotEmpty();
        }
    }
}
