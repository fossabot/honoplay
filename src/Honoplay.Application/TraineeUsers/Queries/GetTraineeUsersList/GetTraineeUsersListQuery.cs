using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList
{
    public class GetTraineeUsersListQuery : IRequest<ResponseModel<TraineeUsersListModel>>
    {
        public GetTraineeUsersListQuery() { }

        public GetTraineeUsersListQuery(int adminUserId, Guid tenantId, int skip = 0, int take = 10)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
            TenantId = tenantId;
        }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTraineeUsersListQueryModel : IRequest<ResponseModel<TraineeUsersListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
