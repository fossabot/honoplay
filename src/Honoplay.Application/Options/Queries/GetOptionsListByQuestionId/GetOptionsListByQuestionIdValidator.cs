using FluentValidation;

namespace Honoplay.Application.Options.Queries.GetOptionsListByQuestionId
{
    public class GetOptionsListByQuestionIdValidator : AbstractValidator<GetOptionsListByQuestionIdQuery>
    {
        public GetOptionsListByQuestionIdValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty()
                .NotNull();
        }
    }
}
