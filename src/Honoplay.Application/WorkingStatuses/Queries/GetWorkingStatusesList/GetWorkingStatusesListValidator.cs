using FluentValidation;

namespace Honoplay.Application.WorkingStatuses.Queries.GetWorkingStatusesList
{
    public class GetWorkingStatusesListValidator : AbstractValidator<GetWorkingStatusesListQueryModel>
    {
        public GetWorkingStatusesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
