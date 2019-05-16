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

        public GetTraineesListQuery(int adminUserId, int skip, int take, Guid tenantId)
        {
            AdminUserId = adminUserId;
            Skip = skip;
            Take = take;
            TenantId = tenantId;
        }

        public int AdminUserId { get; private set; }
        public Guid TenantId { get; set; }
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
