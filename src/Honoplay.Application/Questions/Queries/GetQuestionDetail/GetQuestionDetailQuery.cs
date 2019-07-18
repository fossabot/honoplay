using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQuery : IRequest<ResponseModel<QuestionDetailModel>>
    {
        public GetQuestionDetailQuery(int id, int adminUserId, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetQuestionDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
