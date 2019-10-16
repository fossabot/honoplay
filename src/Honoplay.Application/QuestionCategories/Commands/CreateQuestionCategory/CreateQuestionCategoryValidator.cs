using FluentValidation;

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryValidator : AbstractValidator<CreateQuestionCategoryCommand>
    {
        public CreateQuestionCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
