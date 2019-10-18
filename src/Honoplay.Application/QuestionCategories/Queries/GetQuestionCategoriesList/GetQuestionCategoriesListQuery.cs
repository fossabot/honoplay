using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public class GetQuestionCategoriesListQuery : IRequest<ResponseModel<QuestionCategoriesListModel>>
    {
        public GetQuestionCategoriesListQuery() { }

        public GetQuestionCategoriesListQuery(Guid tenantId, int skip, int take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        [JsonIgnore]
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
    }

    public class GetQuestionCategoriesListQueryModel : IRequest<ResponseModel<QuestionCategoriesListModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
