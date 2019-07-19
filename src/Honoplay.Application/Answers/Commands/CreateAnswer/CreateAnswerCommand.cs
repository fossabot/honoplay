using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommand : IRequest<ResponseModel<CreateAnswerModel>>
    {
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
    }
}
