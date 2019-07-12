using FluentValidation;

namespace Honoplay.Application.Professions.Commands.CreateProfession
{
    public class CreateProfessionValidator : AbstractValidator<CreateProfessionCommand>
    {
        public CreateProfessionValidator()
        {
            RuleFor(x => x.Professions)
                .ForEach(x => x.NotNull().NotEmpty())
                .NotNull()
                .NotEmpty();
        }
    }
}
