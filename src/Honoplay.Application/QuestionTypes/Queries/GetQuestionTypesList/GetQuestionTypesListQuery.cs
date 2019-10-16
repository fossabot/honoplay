using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList
{
    public class GetQuestionTypesListQuery : IRequest<ResponseModel<QuestionTypesListModel>>
    {
        public GetQuestionTypesListQuery(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetQuestionTypesListQueryModel : IRequest<ResponseModel<QuestionTypesListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
