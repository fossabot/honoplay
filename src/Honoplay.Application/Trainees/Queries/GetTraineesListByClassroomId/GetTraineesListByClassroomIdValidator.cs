using FluentValidation;

namespace Honoplay.Application.Trainees.Queries.GetTraineesListByClassroomId
{
    public class GetTraineesListByClassroomIdValidator : AbstractValidator<GetTraineesListByClassroomIdQuery>
    {
        public GetTraineesListByClassroomIdValidator()
        {
            RuleFor(x => x.ClassroomId)
                .NotEmpty()
                .NotNull();
        }
    }
}
