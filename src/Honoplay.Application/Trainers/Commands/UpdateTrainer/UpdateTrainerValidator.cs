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
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ProfessionId)
                .NotEmpty()
                .NotNull();
        }
    }
}
