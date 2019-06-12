using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListQuery : IRequest<ResponseModel<DepartmentsListModel>>
    {
        public GetDepartmentsListQuery(int adminUserId, string hostName, int skip = 0, int take = 10)
        {
            HostName = hostName;
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
        }

        public GetDepartmentsListQuery()
        {

        }

        public int AdminUserId { get; private set; }
        public string HostName { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;

    }
    public class GetDepartmentsListQueryModel : IRequest<ResponseModel<DepartmentsListModel>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
