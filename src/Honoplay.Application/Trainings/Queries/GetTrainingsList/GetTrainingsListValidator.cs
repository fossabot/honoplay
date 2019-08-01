using FluentValidation;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListValidator : AbstractValidator<GetTrainingsListQueryModel>
    {
        public GetTrainingsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
