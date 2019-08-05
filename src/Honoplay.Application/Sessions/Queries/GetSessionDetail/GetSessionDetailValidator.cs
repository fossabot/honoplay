using FluentValidation;

namespace Honoplay.Application.Sessions.Queries.GetSessionDetail
{
    public class GetSessionDetailValidator : AbstractValidator<GetSessionDetailQuery>
    {
        public GetSessionDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
