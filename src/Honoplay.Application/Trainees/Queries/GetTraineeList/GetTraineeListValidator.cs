using FluentValidation;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTraineeListValidator : AbstractValidator<GetTenantsListQuery>
    {
        public GetTraineeListValidator()
        {
            RuleFor(x => x.Skip).NotNull().GreaterThan(-1);
            RuleFor(x => x.Take).NotNull().GreaterThan(4).LessThan(101);
        }
    }
}