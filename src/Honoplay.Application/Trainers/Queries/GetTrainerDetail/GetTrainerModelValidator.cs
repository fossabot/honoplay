using FluentValidation;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerModelValidator : AbstractValidator<GetTrainerDetailQuery>
    {
        public GetTrainerModelValidator()
        {
            RuleFor(x => x.AdminUserId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
