using FluentValidation;

namespace Honoplay.Application.Professions.Queries.GetProfessionsList
{
    public class GetProfessionsListValidator : AbstractValidator<GetProfessionsListQueryModel>
    {
        public GetProfessionsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
