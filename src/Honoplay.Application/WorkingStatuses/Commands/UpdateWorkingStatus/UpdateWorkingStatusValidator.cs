using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusValidator : AbstractValidator<UpdateWorkingStatusCommand>
    {
        public UpdateWorkingStatusValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
