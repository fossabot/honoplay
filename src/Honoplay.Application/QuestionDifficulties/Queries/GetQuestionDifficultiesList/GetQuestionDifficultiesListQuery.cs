using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList
{
    public class GetQuestionDifficultiesListQuery : IRequest<ResponseModel<QuestionDifficultiesListModel>>
    {
        public GetQuestionDifficultiesListQuery(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetQuestionDifficultiesListQueryModel : IRequest<ResponseModel<QuestionDifficultiesListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
