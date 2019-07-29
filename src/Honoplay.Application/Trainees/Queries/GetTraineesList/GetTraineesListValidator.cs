using FluentValidation;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListValidator : AbstractValidator<GetTraineesListQueryModel>
    {
        public GetTraineesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}