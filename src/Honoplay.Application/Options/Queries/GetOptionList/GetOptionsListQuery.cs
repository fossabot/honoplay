using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Options.Queries.GetOptionsList
{
    public class GetOptionsListQuery : IRequest<ResponseModel<OptionsListModel>>
    {
        public GetOptionsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetOptionsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetOptionsListQueryModel : IRequest<ResponseModel<OptionsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
