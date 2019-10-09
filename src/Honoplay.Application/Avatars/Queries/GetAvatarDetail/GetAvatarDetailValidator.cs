using FluentValidation;

namespace Honoplay.Application.Avatars.Queries.GetAvatarDetail
{
    public class GetAvatarDetailValidator : AbstractValidator<GetAvatarDetailQuery>
    {
        public GetAvatarDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
