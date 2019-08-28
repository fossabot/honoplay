using FluentValidation;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUserDetail
{
    public class GetTraineeUserDetailValidator : AbstractValidator<GetTraineeUserDetailQuery>
    {
        public GetTraineeUserDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}