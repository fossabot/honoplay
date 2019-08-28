using FluentValidation;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList
{
    public class GetTraineeUsersListValidator : AbstractValidator<GetTraineeUsersListQueryModel>
    {
        public GetTraineeUsersListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}