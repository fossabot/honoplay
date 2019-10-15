using FluentValidation;

namespace Honoplay.Application.Tags.Queries.GetTagDetail
{
    public class GetTagDetailValidator : AbstractValidator<GetTagDetailQuery>
    {
        public GetTagDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
