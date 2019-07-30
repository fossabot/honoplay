using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public class GetTrainingSeriesesListQuery : IRequest<ResponseModel<TrainingSeriesesListModel>>
    {
        public GetTrainingSeriesesListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetTrainingSeriesesListQuery() { }

        public Guid TenantId { get; }
        public int? Skip { get; }
        public int? Take { get; }

    }
    public class GetTrainingSeriesesListQueryModel : IRequest<ResponseModel<TrainingSeriesesListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
