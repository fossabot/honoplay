using FluentValidation;
#nullable enable

namespace Honoplay.Application.Tokens.Commands
{
    public class GetAdminUserTokenValidator : AbstractValidator<GetAdminUserTokenCommand>
    {
        public GetAdminUserTokenValidator()
        {
            RuleFor(x => x.Username).MaximumLength(50).NotNull().NotEmpty();
            RuleFor(x => x.Password).MaximumLength(50).NotNull().NotEmpty();
        }
    }

}
