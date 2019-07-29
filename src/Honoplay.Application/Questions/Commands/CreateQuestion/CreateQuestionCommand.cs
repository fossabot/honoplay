using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<ResponseModel<CreateQuestionModel>>
    {
        public string Text { get; set; }
        public int Duration { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
}
