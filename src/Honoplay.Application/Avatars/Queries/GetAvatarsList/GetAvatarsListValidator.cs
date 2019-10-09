using FluentValidation;

namespace Honoplay.Application.Avatars.Queries.GetAvatarsList
{
    public class GetAvatarsListValidator : AbstractValidator<GetAvatarsListQueryModel>
    {
        public GetAvatarsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
