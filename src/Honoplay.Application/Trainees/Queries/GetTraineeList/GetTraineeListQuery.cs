using System;
using System.Collections.Generic;
using System.Text;
using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainees.Queries.GetTraineeList
{
    public class GetTraineeListQuery : IRequest<ResponseModel<TraineeListModel>>
    {
        public GetTraineeListQuery()
        {
        }

        public GetTraineeListQuery(int adminUserId, int skip = 0, int take = 10)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public int AdminUserId { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
    }

    public class GetTraineeListQueryModel : IRequest<ResponseModel<TraineeListModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
