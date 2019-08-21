using FluentValidation;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId
{
    public class GetTrainingsListByTrainingSeriesIdValidator : AbstractValidator<GetTrainingsListByTrainingSeriesIdQuery>
    {
        public GetTrainingsListByTrainingSeriesIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
