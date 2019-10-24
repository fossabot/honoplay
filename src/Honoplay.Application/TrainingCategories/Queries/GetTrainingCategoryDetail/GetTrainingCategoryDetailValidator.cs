using FluentValidation;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public class GetTrainingCategoryDetailValidator : AbstractValidator<GetTrainingCategoryDetailQuery>
    {
        public GetTrainingCategoryDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
