using System;
using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQuery : IRequest<ResponseModel<TrainersListModel>>
    {
        public GetTrainersListQuery(int adminUserId, string hostName, int skip = 0, int take = 10)
        {
            HostName = hostName;
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public GetTrainersListQuery()
        {

        }

        public int AdminUserId { get; private set; }
        public string HostName { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTrainersListQueryModel : IRequest<ResponseModel<TrainersListModel>>
    {
        public string HostName { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
