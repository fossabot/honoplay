using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public class GetTrainingCategoryDetailQuery : IRequest<ResponseModel<TrainingCategoryDetailModel>>
    {
        public GetTrainingCategoryDetailQuery(int id)
        {
            Id = id;
        }

        public GetTrainingCategoryDetailQuery() { }

        public int Id { get; private set; }
    }
}