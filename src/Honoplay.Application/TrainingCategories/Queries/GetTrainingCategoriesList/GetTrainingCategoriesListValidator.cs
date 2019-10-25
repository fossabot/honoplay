using FluentValidation;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList
{
    public class GetTrainingCategoriesListValidator : AbstractValidator<GetTrainingCategoriesListQueryModel>
    {
        public GetTrainingCategoriesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
