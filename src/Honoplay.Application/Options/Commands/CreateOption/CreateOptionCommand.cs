using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionCommand : IRequest<ResponseModel<CreateOptionModel>>
    {
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int? VisibilityOrder { get; set; }
        public bool IsCorrect { get; set; }
    }
}
