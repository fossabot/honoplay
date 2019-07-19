using Honoplay.Application.Answers.Commands.CreateAnswer;
using MediatR;
using Newtonsoft.Json;
using System;
using Honoplay.Application._Infrastructure;

namespace Honoplay.Application.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerCommand : IRequest<ResponseModel<UpdateAnswerModel>>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
    }
}
