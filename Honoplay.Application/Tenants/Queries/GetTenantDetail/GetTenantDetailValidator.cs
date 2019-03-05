using FluentValidation;

#nullable enable

namespace Honoplay.Application.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailValidator : AbstractValidator<TenantDetailModel>
    {
        public GetTenantDetailValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}