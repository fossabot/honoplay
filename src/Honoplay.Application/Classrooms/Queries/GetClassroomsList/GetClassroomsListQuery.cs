using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListQuery : IRequest<ResponseModel<ClassroomsListModel>>
    {
        public GetClassroomsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetClassroomsListQuery() { }

        public Guid TenantId { get; }
        public int? Skip { get; }
        public int? Take { get; }

    }
    public class GetClassroomsListQueryModel : IRequest<ResponseModel<ClassroomsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
