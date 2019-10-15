using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest<ResponseModel<UpdateTagModel>>
    {

        public int Id { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public bool ToQuestion { get; set; }

    }
}
