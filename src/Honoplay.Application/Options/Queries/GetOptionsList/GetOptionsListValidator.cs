using FluentValidation;

namespace Honoplay.Application.Options.Queries.GetOptionsList
{
    public class GetOptionsListValidator : AbstractValidator<GetOptionsListQueryModel>
    {
        public GetOptionsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
