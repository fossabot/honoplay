using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQuery : IRequest<ResponseModel<TrainersListModel>>
    {
        public GetTrainersListQuery(int adminUserId, Guid tenantId, int skip = 0, int take = 10)
        {
            TenantId = tenantId;
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public GetTrainersListQuery()
        {

        }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTrainersListQueryModel : IRequest<ResponseModel<TrainersListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
