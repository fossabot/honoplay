using FluentValidation;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersListByClassroomId
{
    public class GetTraineeUsersListByClassroomIdValidator : AbstractValidator<GetTraineeUsersListByClassroomIdQuery>
    {
        public GetTraineeUsersListByClassroomIdValidator()
        {
            RuleFor(x => x.ClassroomId)
                .NotEmpty()
                .NotNull();
        }
    }
}
