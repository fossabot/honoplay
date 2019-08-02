using FluentValidation;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomDetail
{
    public class GetClassroomDetailValidator : AbstractValidator<GetClassroomDetailQuery>
    {
        public GetClassroomDetailValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
