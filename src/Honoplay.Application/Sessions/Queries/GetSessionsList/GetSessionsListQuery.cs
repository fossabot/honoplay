using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Sessions.Queries.GetSessionsList
{
    public class GetSessionsListQuery : IRequest<ResponseModel<SessionsListModel>>
    {
        public GetSessionsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetSessionsListQuery() { }

        public Guid TenantId { get; }
        public int? Skip { get; }
        public int? Take { get; }

    }
    public class GetSessionsListQueryModel : IRequest<ResponseModel<SessionsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
