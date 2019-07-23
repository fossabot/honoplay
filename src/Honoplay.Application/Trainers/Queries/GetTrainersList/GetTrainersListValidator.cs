using FluentValidation;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListValidator : AbstractValidator<GetTrainersListQueryModel>
    {
        public GetTrainersListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
