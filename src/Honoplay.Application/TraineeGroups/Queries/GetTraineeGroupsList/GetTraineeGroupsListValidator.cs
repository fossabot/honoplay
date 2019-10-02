using FluentValidation;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList
{
    public class GetTraineeGroupsListValidator : AbstractValidator<GetTraineeGroupsListQueryModel>
    {
        public GetTraineeGroupsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
