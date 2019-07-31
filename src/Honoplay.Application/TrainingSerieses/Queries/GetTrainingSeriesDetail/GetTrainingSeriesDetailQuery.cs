using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesDetail
{
    public class GetTrainingSeriesDetailQuery : IRequest<ResponseModel<TrainingSeriesDetailModel>>
    {
        public GetTrainingSeriesDetailQuery(int adminUserId, int id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTrainingSeriesDetailQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
