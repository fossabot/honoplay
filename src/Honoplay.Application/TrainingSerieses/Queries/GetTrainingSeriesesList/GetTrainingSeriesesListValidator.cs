using FluentValidation;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public class GetTrainingSeriesesListValidator : AbstractValidator<GetTrainingSeriesesListQueryModel>
    {
        public GetTrainingSeriesesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
