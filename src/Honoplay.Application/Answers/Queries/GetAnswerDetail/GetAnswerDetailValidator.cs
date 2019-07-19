using FluentValidation;

namespace Honoplay.Application.Answers.Queries.GetAnswerDetail
{
    public class GetAnswerDetailValidator : AbstractValidator<GetAnswerDetailQuery>
    {
        public GetAnswerDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
