using FluentValidation;

namespace Honoplay.Application.TrainerUsers.Commands.CreateTrainerUser
{
    public class CreateTrainerUserValidator : AbstractValidator<CreateTrainerUserCommand>
    {
        public CreateTrainerUserValidator()
        {
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

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);

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
