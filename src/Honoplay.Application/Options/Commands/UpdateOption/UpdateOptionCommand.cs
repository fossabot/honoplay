using Honoplay.Application.Options.Commands.CreateOption;
using MediatR;
using Newtonsoft.Json;
using System;
using Honoplay.Application._Infrastructure;

namespace Honoplay.Application.Options.Commands.UpdateOption
{
    public class UpdateOptionCommand : IRequest<ResponseModel<UpdateOptionModel>>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int VisibilityOrder { get; set; }
    }
}
