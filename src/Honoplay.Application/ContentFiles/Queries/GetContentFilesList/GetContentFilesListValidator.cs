using FluentValidation;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFilesList
{
    public class GetContentFilesListValidator : AbstractValidator<GetContentFilesListQueryModel>
    {
        public GetContentFilesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
