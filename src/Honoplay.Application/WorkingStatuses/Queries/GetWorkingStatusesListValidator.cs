using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Queries
{
    public class GetWorkingStatusesListValidator : AbstractValidator<GetWorkingStatusesListQueryModel>
    {
        public GetWorkingStatusesListValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .WithMessage("NotNullValidator");

            RuleFor(x => x.Skip)
                .GreaterThan(-1)
                .WithMessage("GreaterThanValidator");

            RuleFor(x => x.Take)
                .NotNull()
                .WithMessage("NotNullValidator");

            RuleFor(x => x.Take)
                .GreaterThan(1)
                .WithMessage("GreaterThanValidator");

            RuleFor(x => x.Take)
                .LessThan(101)
                .WithMessage("LessThanValidator");
        }
    }
}
