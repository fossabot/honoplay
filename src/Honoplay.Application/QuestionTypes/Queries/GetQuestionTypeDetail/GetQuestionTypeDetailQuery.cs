using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailQuery : IRequest<ResponseModel<QuestionTypeDetailModel>>
    {
        public GetQuestionTypeDetailQuery(int id)
        {
            Id = id;
        }

        public GetQuestionTypeDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}