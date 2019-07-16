using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusValidator : AbstractValidator<UpdateWorkingStatusCommand>
    {
        public UpdateWorkingStatusValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

        }
    }
}
