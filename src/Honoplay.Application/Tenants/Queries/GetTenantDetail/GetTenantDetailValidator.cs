using FluentValidation;

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailValidator : AbstractValidator<GetTenantDetailQuery>
    {
        public GetTenantDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}