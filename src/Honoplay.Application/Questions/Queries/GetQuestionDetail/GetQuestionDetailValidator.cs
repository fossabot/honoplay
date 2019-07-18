using FluentValidation;

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailValidator : AbstractValidator<GetQuestionDetailQuery>
    {
        public GetQuestionDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
