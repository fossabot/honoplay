using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId
{
    public class GetSessionsListByClassroomIdQuery : IRequest<ResponseModel<SessionsListByClassroomIdModel>>
    {
        public GetSessionsListByClassroomIdQuery(int classroomId, Guid tenantId)
        {
            ClassroomId = classroomId;
            TenantId = tenantId;
        }

        public GetSessionsListByClassroomIdQuery() { }

        public int ClassroomId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
