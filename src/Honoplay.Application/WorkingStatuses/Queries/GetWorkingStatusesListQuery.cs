using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.WorkingStatuses.Queries
{
    public class GetWorkingStatusesListQuery : IRequest<ResponseModel<WorkingStatusesListModel>>
    {
        public GetWorkingStatusesListQuery() { }

        public GetWorkingStatusesListQuery(int adminUserId, Guid tenantId, int skip, int take)
        {
            AdminUserId = adminUserId;
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
    }

    public class GetWorkingStatusesListQueryModel : IRequest<ResponseModel<WorkingStatusesListModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
