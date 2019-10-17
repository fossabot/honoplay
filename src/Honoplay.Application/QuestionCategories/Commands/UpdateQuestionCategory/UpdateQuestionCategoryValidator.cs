using FluentValidation;

namespace Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory
{
    public class UpdateQuestionCategoryValidator : AbstractValidator<UpdateQuestionCategoryCommand>
    {
        public UpdateQuestionCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

        }
    }
}
