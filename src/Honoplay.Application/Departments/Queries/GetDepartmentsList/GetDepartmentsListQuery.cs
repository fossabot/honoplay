using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListQuery : IRequest<ResponseModel<DepartmentsListModel>>
    {
        public GetDepartmentsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetDepartmentsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetDepartmentsListQueryModel : IRequest<ResponseModel<DepartmentsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
