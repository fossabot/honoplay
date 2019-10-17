using FluentValidation;

namespace Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public class GetQuestionCategoriesListValidator : AbstractValidator<GetQuestionCategoriesListQueryModel>
    {
        public GetQuestionCategoriesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
