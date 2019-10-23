using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList
{
    public class GetTrainingCategoriesListQuery : IRequest<ResponseModel<TrainingCategoriesListModel>>
    {
        public GetTrainingCategoriesListQuery(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetTrainingCategoriesListQueryModel : IRequest<ResponseModel<TrainingCategoriesListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}