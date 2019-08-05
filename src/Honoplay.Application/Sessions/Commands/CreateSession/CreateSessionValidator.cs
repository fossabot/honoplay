using FluentValidation;

namespace Honoplay.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionValidator()
        {
            RuleForEach(x => x.CreateSessionModels)
                .SetValidator(new InlineValidator<CreateSessionCommandModel> {
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.Name)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(50),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.ClassroomId)
                        .NotNull()
                        .NotEmpty(),
                    inlineValidator => inlineValidator
                        .RuleFor(x => x.GameId)
                        .NotNull()
                        .NotEmpty()
                });
        }
    }
}
