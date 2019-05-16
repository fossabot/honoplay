using System;
using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQuery : IRequest<ResponseModel<TrainersListModel>>
    {
        public GetTrainersListQuery(int adminUserId, Guid tenantId, int skip, int take)
        {
            TenantId = tenantId;
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public GetTrainersListQuery()
        {

        }

        public int AdminUserId { get; private set; }
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
    }

    public class GetTrainersListQueryModel : IRequest<ResponseModel<TrainersListModel>>
    {
        public Guid TenantId { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
