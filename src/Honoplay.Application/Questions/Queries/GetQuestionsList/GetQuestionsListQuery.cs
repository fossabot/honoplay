using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQuery : IRequest<ResponseModel<QuestionsListModel>>
    {
        public GetQuestionsListQuery() { }

        public GetQuestionsListQuery(int createdBy, Guid tenantId, int? skip, int? take)
        {
            CreatedBy = createdBy;
            Skip = skip;
            Take = take;
            TenantId = tenantId;
        }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }

    public class GetQuestionsListQueryModel : IRequest<ResponseModel<QuestionsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
