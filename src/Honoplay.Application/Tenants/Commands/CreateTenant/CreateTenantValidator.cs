using FluentValidation;

#nullable enable

namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(1).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.HostName).MinimumLength(1).MaximumLength(150).NotNull().NotEmpty();
            RuleFor(x => x.Description).MaximumLength(250);
        }
    }

}
