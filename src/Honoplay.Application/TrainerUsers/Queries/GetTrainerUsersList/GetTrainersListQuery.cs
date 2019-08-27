using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList
{
    public class GetTrainerUsersListQuery : IRequest<ResponseModel<TrainerUsersListModel>>
    {
        public GetTrainerUsersListQuery(int adminUserId, Guid tenantId, int skip = 0, int take = 10)
        {
            TenantId = tenantId;
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public GetTrainerUsersListQuery()
        {

        }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTrainerUsersListQueryModel : IRequest<ResponseModel<TrainerUsersListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
