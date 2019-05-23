using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Trainees.Queries.GetTraineeList
{
    public class GetTraineesListQuery : IRequest<ResponseModel<TraineesListModel>>
    {
        public GetTraineesListQuery()
        {
        }

        public GetTraineesListQuery(int adminUserId, Guid tenantId, int skip = 0, int take = 10)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
            TenantId = tenantId;
        }

        public int AdminUserId { get; private set; }
        public Guid TenantId { get; private set; }
        public int Skip { get; private set; } = 0;
        public int Take { get; private set; } = 10;
    }

    public class GetTraineesListQueryModel : IRequest<ResponseModel<TraineesListModel>>
    {
        public Guid TenantId { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
