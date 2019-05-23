using FluentValidation;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeValidator : AbstractValidator<CreateTraineeCommand>
    {
        public CreateTraineeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.NationalIdentityNumber)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.WorkingStatusId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Gender)
                .NotEmpty()
                .NotNull()
                .GreaterThan(-1)
                .LessThan(2);

            RuleFor(x => x.DepartmentId).NotEmpty().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(150).MinimumLength(1);
        }
    }
}
