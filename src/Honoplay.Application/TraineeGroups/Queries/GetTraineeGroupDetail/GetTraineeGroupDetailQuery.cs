using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public class GetTraineeGroupDetailQuery : IRequest<ResponseModel<TraineeGroupDetailModel>>
    {
        public GetTraineeGroupDetailQuery(int id, int adminUserId, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTraineeGroupDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
