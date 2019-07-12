using FluentValidation;

namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);

            RuleFor(x => x.HostName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }

}
