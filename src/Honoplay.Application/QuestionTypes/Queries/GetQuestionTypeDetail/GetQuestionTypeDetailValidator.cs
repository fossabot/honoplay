using FluentValidation;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailValidator : AbstractValidator<GetQuestionTypeDetailQuery>
    {
        public GetQuestionTypeDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
