using FluentValidation;

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

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
