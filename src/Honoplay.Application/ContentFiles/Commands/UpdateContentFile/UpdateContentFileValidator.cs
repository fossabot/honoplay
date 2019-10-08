using FluentValidation;

namespace Honoplay.Application.ContentFiles.Commands.UpdateContentFile
{
    public class UpdateContentFileValidator : AbstractValidator<UpdateContentFileCommand>
    {
        public UpdateContentFileValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Data)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ContentType)
                .NotNull()
                .NotEmpty();
        }
    }
}
