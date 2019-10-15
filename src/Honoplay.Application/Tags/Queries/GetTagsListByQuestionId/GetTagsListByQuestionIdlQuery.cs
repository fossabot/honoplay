using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Tags.Queries.GetTagsListByQuestionId
{
    public class GetTagsListByQuestionIdQuery : IRequest<ResponseModel<TagsListByQuestionIdModel>>
    {
        public GetTagsListByQuestionIdQuery(int questionId, Guid tenantId)
        {
            QuestionId = questionId;
            TenantId = tenantId;
        }

        public GetTagsListByQuestionIdQuery()
        {

        }

        public int QuestionId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
