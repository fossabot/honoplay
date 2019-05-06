using FluentValidation;

namespace Honoplay.Application.Tenants.Queries.GetTraineeDetail
{
    public class GetTraineeDetailValidator : AbstractValidator<GetTraineeDetailQuery>
    {
        public GetTraineeDetailValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}