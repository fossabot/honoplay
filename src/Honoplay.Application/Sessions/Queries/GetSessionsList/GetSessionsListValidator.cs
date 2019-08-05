using FluentValidation;

namespace Honoplay.Application.Sessions.Queries.GetSessionsList
{
    public class GetSessionsListValidator : AbstractValidator<GetSessionsListQueryModel>
    {
        public GetSessionsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
