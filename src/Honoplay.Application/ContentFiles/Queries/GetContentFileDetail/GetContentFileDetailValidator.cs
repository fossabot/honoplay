using FluentValidation;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFileDetail
{
    public class GetContentFileDetailValidator : AbstractValidator<GetContentFileDetailQuery>
    {
        public GetContentFileDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
