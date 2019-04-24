using FluentValidation;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListValidator : AbstractValidator<GetTenantsListQuery>
    {
        public GetTenantsListValidator()
        {
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(4).LessThan(101);
        }
    }
}