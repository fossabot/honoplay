using FluentValidation;

namespace Honoplay.Application.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListValidator : AbstractValidator<GetDepartmentsListQueryModel>
    {
        public GetDepartmentsListValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .NotEmpty()
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
