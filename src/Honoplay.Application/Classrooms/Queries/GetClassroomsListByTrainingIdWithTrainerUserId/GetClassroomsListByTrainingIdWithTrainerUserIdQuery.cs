using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingIdWithTrainerUserId
{
    public class GetClassroomsListByTrainingIdWithTrainerUserIdQuery : IRequest<ResponseModel<ClassroomsListByTrainingIdWithTrainerUserIdModel>>
    {
        public GetClassroomsListByTrainingIdWithTrainerUserIdQuery(Guid tenantId, int trainingId, int trainerUserId)
        {
            TenantId = tenantId;
            TrainingId = trainingId;
            TrainerUserId = trainerUserId;
        }

        public GetClassroomsListByTrainingIdWithTrainerUserIdQuery() { }

        [JsonIgnore]
        public Guid TenantId { get; }
        [JsonIgnore]
        public int TrainerUserId { get; }
        public int TrainingId { get; set; }

    }
}
