using FluentValidation;
using Honoplay.Application.Trainers.Queries.GetTrainersList;

namespace Honoplay.Application.Tenants.Queries.GetTenantsList
{
    public class GetTrainersListValidator : AbstractValidator<GetTrainersListQueryModel>
    {
        public GetTrainersListValidator()
        {
            RuleFor(x => x.TenantId)
                .NotNull()
                .NotEmpty();

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