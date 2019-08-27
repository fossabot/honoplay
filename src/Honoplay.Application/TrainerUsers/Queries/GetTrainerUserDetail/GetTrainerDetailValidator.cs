using FluentValidation;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUserDetail
{
    public class GetTrainerUserDetailValidator : AbstractValidator<GetTrainerUserDetailQuery>
    {
        public GetTrainerUserDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
