using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersListByClassroomId
{
    public class GetTraineeUsersListByClassroomIdQuery : IRequest<ResponseModel<GetTraineeUsersListByClassroomIdModel>>
    {
        public GetTraineeUsersListByClassroomIdQuery(int adminUserId, int classroomId, Guid tenantId)
        {
            ClassroomId = classroomId;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetTraineeUsersListByClassroomIdQuery() { }

        public int ClassroomId { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
