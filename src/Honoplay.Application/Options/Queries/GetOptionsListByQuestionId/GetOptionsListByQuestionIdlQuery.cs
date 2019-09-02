using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Options.Queries.GetOptionsListByQuestionId
{
    public class GetOptionsListByQuestionIdQuery : IRequest<ResponseModel<OptionsListByQuestionIdModel>>
    {
        public GetOptionsListByQuestionIdQuery(int questionId, Guid tenantId)
        {
            QuestionId = questionId;
            TenantId = tenantId;
        }

        public GetOptionsListByQuestionIdQuery()
        {

        }

        public int QuestionId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
