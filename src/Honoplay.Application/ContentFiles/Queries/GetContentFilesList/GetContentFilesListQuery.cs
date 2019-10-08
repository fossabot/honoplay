using Honoplay.Application._Infrastructure;
using MediatR;
using System;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFilesList
{
    public class GetContentFilesListQuery : IRequest<ResponseModel<ContentFilesListModel>>
    {
        public GetContentFilesListQuery(Guid tenantId, int? skip, int? take)
        {
            TenantId = tenantId;
            Skip = skip;
            Take = take;
        }

        public Guid TenantId { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetContentFilesListQueryModel : IRequest<ResponseModel<ContentFilesListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
