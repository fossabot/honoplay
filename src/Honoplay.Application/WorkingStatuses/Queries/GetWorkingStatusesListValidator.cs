using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Queries
{
    public class GetWorkingStatusesListValidator : AbstractValidator<GetWorkingStatusesListQueryModel>
    {
        public GetWorkingStatusesListValidator()
        {
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
