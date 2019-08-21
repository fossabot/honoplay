using FluentValidation;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId
{
    public class GetClassroomsListByTrainingIdValidator : AbstractValidator<GetClassroomsListByTrainingIdQuery>
    {
        public GetClassroomsListByTrainingIdValidator()
        {
            RuleFor(x => x.TrainingId)
                .NotNull()
                .NotEmpty();
        }
    }
}
