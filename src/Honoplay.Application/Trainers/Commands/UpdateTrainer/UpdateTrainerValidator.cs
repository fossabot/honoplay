using FluentValidation;

namespace Honoplay.Application.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerValidator : AbstractValidator<UpdateTrainerCommand>
    {
        public UpdateTrainerValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProfessionId)
                .NotEmpty()
                .NotNull();
        }
    }
}
