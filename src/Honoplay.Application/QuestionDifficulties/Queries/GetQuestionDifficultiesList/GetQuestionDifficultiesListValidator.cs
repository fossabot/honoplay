using FluentValidation;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList
{
    public class GetQuestionDifficultiesListValidator : AbstractValidator<GetQuestionDifficultiesListQueryModel>
    {
        public GetQuestionDifficultiesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
