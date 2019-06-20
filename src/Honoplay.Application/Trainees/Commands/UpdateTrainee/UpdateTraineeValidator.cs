using FluentValidation;

namespace Honoplay.Application.Trainees.Commands.UpdateTrainee
{
    public class UpdateTraineeValidator : AbstractValidator<UpdateTraineeCommand>
    {
        public UpdateTraineeValidator()
        {
            RuleFor(x => x.Id).NotNull();

            RuleFor(x => x.Id).NotEmpty();


            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Surname).NotNull();
            RuleFor(x => x.Surname).NotEmpty();

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

            RuleFor(x => x.Gender).NotNull();
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.Gender).GreaterThan(-1);
            RuleFor(x => x.Gender).LessThan(2);

            RuleFor(x => x.DepartmentId).NotNull().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(150).MinimumLength(1);
        }
    }
}
