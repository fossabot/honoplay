using FluentValidation;

namespace Honoplay.Application.Tags.Queries.GetTagsListByQuestionId
{
    public class GetTagsListByQuestionIdValidator : AbstractValidator<GetTagsListByQuestionIdQuery>
    {
        public GetTagsListByQuestionIdValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty()
                .NotNull();
        }
    }
}
