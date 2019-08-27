using FluentValidation;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainerUserId
{
    public class GetTrainingsListByTrainerUserIdValidator : AbstractValidator<GetTrainingsListByTrainerUserIdQuery>
    {
        public GetTrainingsListByTrainerUserIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
