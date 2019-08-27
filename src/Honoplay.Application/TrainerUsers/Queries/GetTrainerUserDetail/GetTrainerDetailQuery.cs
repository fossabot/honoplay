using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUserDetail
{
    public class GetTrainerUserDetailQuery : IRequest<ResponseModel<TrainerUserDetailModel>>
    {
        public GetTrainerUserDetailQuery(int adminUserId, int id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTrainerUserDetailQuery()
        {

        }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
