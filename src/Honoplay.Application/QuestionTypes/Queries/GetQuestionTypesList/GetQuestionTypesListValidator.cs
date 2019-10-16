using FluentValidation;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList
{
    public class GetQuestionTypesListValidator : AbstractValidator<GetQuestionTypesListQueryModel>
    {
        public GetQuestionTypesListValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThan(-1);

            RuleFor(x => x.Take)
                .GreaterThan(4)
                .LessThan(101);
        }
    }
}
