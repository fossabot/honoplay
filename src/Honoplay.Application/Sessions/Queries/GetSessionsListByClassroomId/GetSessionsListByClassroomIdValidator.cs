using FluentValidation;

namespace Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId
{
    public class GetSessionsListByClassroomIdValidator : AbstractValidator<GetSessionsListByClassroomIdQuery>
    {
        public GetSessionsListByClassroomIdValidator()
        {
            RuleFor(x => x.ClassroomId)
                .NotEmpty()
                .NotNull();
        }
    }
}
