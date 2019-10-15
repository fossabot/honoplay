using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<ResponseModel<TagsListModel>>
    {
        public GetTagsListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public GetTagsListQuery() { }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetTagsListQueryModel : IRequest<ResponseModel<TagsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
