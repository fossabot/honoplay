using FluentValidation;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListValidator : AbstractValidator<GetClassroomsListQueryModel>
    {
        public GetClassroomsListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
