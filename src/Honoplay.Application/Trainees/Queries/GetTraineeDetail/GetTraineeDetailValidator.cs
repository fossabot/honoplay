using FluentValidation;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailValidator : AbstractValidator<GetTraineeDetailQuery>
    {
        public GetTraineeDetailValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}