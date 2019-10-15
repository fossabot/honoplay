using FluentValidation;

namespace Honoplay.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListValidator : AbstractValidator<GetTagsListQueryModel>
    {
        public GetTagsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
