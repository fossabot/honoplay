using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Professions.Queries.GetProfessionsList
{
    public class GetProfessionsListQuery : IRequest<ResponseModel<ProfessionsListModel>>
    {
        public GetProfessionsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetProfessionsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetProfessionsListQueryModel : IRequest<ResponseModel<ProfessionsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
