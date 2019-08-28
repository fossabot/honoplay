using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUserDetail
{
    public class GetTraineeUserDetailQuery : IRequest<ResponseModel<TraineeUserDetailModel>>
    {
        public GetTraineeUserDetailQuery(int id, int adminUserId, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTraineeUserDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}