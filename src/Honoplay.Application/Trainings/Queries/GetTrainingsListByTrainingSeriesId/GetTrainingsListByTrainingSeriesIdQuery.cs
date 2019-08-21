using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId
{
    public class GetTrainingsListByTrainingSeriesIdQuery : IRequest<ResponseModel<GetTrainingsListByTrainingSeriesIdModel>>
    {
        public GetTrainingsListByTrainingSeriesIdQuery(int adminUserId, int id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTrainingsListByTrainingSeriesIdQuery() { }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
