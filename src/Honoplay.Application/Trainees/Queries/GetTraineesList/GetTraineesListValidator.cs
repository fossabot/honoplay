using FluentValidation;
using Honoplay.Application.Trainees.Queries.GetTraineeList;

namespace Honoplay.Application.Tenants.Queries.GetTrainersList
{
    public class GetTraineesListValidator : AbstractValidator<GetTraineesListQueryModel>
    {
        public GetTraineesListValidator()
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