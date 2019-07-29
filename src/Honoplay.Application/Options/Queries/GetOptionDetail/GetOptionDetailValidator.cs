using FluentValidation;

namespace Honoplay.Application.Options.Queries.GetOptionDetail
{
    public class GetOptionDetailValidator : AbstractValidator<GetOptionDetailQuery>
    {
        public GetOptionDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
