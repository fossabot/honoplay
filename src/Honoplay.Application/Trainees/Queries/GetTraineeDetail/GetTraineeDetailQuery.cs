using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQuery : IRequest<ResponseModel<TraineeDetailModel>>
    {
        public GetTraineeDetailQuery(int id, int adminUserId, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTraineeDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}