using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFileDetail
{
    public class GetContentFileDetailQuery : IRequest<ResponseModel<ContentFileDetailModel>>
    {
        public GetContentFileDetailQuery(int adminUserId, Guid id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetContentFileDetailQuery() { }

        public Guid Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
