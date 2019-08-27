using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Trainees.Queries.GetTraineesListByClassroomId
{
    public class GetTraineesListByClassroomIdQuery : IRequest<ResponseModel<GetTraineesListByClassroomIdModel>>
    {
        public GetTraineesListByClassroomIdQuery(int adminUserId, int classroomId, Guid tenantId)
        {
            ClassroomId = classroomId;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTraineesListByClassroomIdQuery() { }

        public int ClassroomId { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
