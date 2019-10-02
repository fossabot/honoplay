using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList
{
    public class GetTraineeGroupsListQuery : IRequest<ResponseModel<TraineeGroupsListModel>>
    {
        public GetTraineeGroupsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetTraineeGroupsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetTraineeGroupsListQueryModel : IRequest<ResponseModel<TraineeGroupsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
