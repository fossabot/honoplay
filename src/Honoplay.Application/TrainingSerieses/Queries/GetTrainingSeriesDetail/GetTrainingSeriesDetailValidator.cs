using FluentValidation;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesDetail
{
    public class GetTrainingSeriesDetailValidator : AbstractValidator<GetTrainingSeriesDetailQuery>
    {
        public GetTrainingSeriesDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
