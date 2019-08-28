using FluentValidation;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingIdWithTrainerUserId
{
    public class GetClassroomsListByTrainingIdWithTrainerUserIdValidator : AbstractValidator<GetClassroomsListByTrainingIdWithTrainerUserIdQuery>
    {
        public GetClassroomsListByTrainingIdWithTrainerUserIdValidator()
        {
            RuleFor(x => x.TrainingId)
                .NotNull()
                .NotEmpty();
        }
    }
}
