using FluentValidation;

#nullable enable

namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).MinimumLength(1).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.HostName).MinimumLength(1).MaximumLength(150).NotNull().NotEmpty();
            RuleFor(x => x.Description).MaximumLength(250);
        }
    }

}
