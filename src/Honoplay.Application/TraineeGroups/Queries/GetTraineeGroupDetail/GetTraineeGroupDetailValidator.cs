using FluentValidation;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public class GetTraineeGroupDetailValidator : AbstractValidator<GetTraineeGroupDetailQuery>
    {
        public GetTraineeGroupDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
