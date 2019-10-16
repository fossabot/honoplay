using FluentValidation;

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryValidator : AbstractValidator<CreateQuestionCategoryCommand>
    {
        public CreateQuestionCategoryValidator()
        {
            {
                RuleForEach(x => x.CreateQuestionCategoryModels).SetValidator(
                    new InlineValidator<CreateQuestionCategoryCommandModel>
                    {
                        orderValidator => orderValidator.RuleFor(x => x.Name).NotNull().NotEmpty()
                    });
            }
        }
    }
}
