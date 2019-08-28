using FluentValidation;

namespace Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser
{
    public class UpdateTraineeUserValidator : AbstractValidator<UpdateTraineeUserCommand>
    {
        public UpdateTraineeUserValidator()
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

            RuleFor(x => x.NationalIdentityNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);

            RuleFor(x => x.WorkingStatusId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Gender)
                .NotNull()
                .GreaterThan(-1)
                .LessThan(2);
        }
    }
}
