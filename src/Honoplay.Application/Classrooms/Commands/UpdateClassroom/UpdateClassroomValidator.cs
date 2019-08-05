using FluentValidation;

namespace Honoplay.Application.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomValidator : AbstractValidator<UpdateClassroomCommand>
    {
        public UpdateClassroomValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.TrainerId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TrainingId)
                .NotNull()
                .NotEmpty();
        }
    }
}
