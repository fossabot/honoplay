using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Options.Queries.GetOptionDetail
{
    public class GetOptionDetailQuery : IRequest<ResponseModel<OptionDetailModel>>
    {
        public GetOptionDetailQuery(int adminUserId, int id, Guid tenantId)
        {
            Id = id;
            AdminUserId = adminUserId;
            TenantId = tenantId;
        }

        public GetOptionDetailQuery()
        {

        }

        public int Id { get; private set; }
        [JsonIgnore]
        public int AdminUserId { get; private set; }
        [JsonIgnore]
        public Guid TenantId { get; private set; }
    }
}
