using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusValidator : AbstractValidator<CreateWorkingStatusCommand>
    {
        public CreateWorkingStatusValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
