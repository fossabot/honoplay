using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<ResponseModel<List<CreateTagModel>>>
    {
        public List<CreateTagCommandModel> CreateTagModels { get; set; }

        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateTagCommandModel
    {
        public string Name { get; set; }
        public bool ToQuestion { get; set; }
    }
}
