using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainerUserId
{
    public class GetTrainingsListByTrainerUserIdQuery : IRequest<ResponseModel<GetTrainingsListByTrainerUserIdModel>>
    {
        public GetTrainingsListByTrainerUserIdQuery(int trainerUserId, Guid tenantId)
        {
            TrainerUserId = trainerUserId;
            TenantId = tenantId;
        }

        public GetTrainingsListByTrainerUserIdQuery() { }

        [JsonIgnore]
        public int TrainerUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
