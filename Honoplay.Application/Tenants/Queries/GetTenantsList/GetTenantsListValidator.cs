using FluentValidation;

#nullable enable

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTenantDetailValidator : AbstractValidator<GetTenantsListQuery>
    {
        public GetTenantDetailValidator()
        {
            RuleFor(x => x.Skip).NotNull().NotEmpty();
            RuleFor(x => x.Take).GreaterThan(1).LessThan(100).NotNull().NotEmpty();
        }
    }
}