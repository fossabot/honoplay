using FluentValidation;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailValidator : AbstractValidator<GetTrainerDetailQuery>
    {
        public GetTrainerDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
