using FluentValidation;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList
{
    public class GetTrainerUsersListValidator : AbstractValidator<GetTrainerUsersListQueryModel>
    {
        public GetTrainerUsersListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
