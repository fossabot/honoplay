using FluentValidation;

namespace Honoplay.Application.ContentFiles.Commands.CreateContentFile
{
    public class CreateContentFileValidator : AbstractValidator<CreateContentFileCommand>
    {
        public CreateContentFileValidator()
        {
            RuleForEach(x => x.CreateContentFileModels).SetValidator(new InlineValidator<CreateContentFileCommandModel> {
                orderValidator => orderValidator.RuleFor(x => x.Name).NotNull().NotEmpty(),
                orderValidator => orderValidator.RuleFor(x => x.Data).NotNull().NotEmpty(),
                orderValidator => orderValidator.RuleFor(x => x.ContentType).NotNull().NotEmpty()
            });

        }

    }
}
