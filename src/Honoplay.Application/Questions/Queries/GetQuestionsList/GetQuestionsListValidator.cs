using FluentValidation;

namespace Honoplay.Application.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListValidator : AbstractValidator<GetQuestionsListQueryModel>
    {
        public GetQuestionsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
