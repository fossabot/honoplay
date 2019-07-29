using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Options.Commands.CreateOption
{
    public class CreateOptionCommand : IRequest<ResponseModel<List<CreateOptionModel>>>
    {
        public List<CreateOptionCommandModel> CreateOptionModels { get; set; }

        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateOptionCommandModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int? VisibilityOrder { get; set; }
        public bool IsCorrect { get; set; }
    }
}
