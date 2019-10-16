using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public class GetQuestionDifficultyDetailQuery : IRequest<ResponseModel<QuestionDifficultyDetailModel>>
    {
        public GetQuestionDifficultyDetailQuery(int id)
        {
            Id = id;
        }

        public GetQuestionDifficultyDetailQuery() { }

        public int Id { get; private set; }
    }
}