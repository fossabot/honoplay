using FluentValidation;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListValidator : AbstractValidator<GetTrainersListQueryModel>
    {
        public GetTrainersListValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .NotNull()
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
