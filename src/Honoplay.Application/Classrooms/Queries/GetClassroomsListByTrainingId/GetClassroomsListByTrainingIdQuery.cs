using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingId
{
    public class GetClassroomsListByTrainingIdQuery : IRequest<ResponseModel<ClassroomsListByTrainingIdModel>>
    {
        public GetClassroomsListByTrainingIdQuery(Guid tenantId, int trainingId)
        {
            TenantId = tenantId;
            TrainingId = trainingId;
        }

        public GetClassroomsListByTrainingIdQuery() { }

        [JsonIgnore]
        public Guid TenantId { get; }
        public int TrainingId { get; set; }

    }
}
