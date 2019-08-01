using FluentValidation;

namespace Honoplay.Application.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailValidator : AbstractValidator<GetTrainingDetailQuery>
    {
        public GetTrainingDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
