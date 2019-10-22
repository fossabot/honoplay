using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<ResponseModel<UpdateQuestionModel>>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionDifficultyId { get; set; }
        public int? QuestionCategoryId { get; set; }
        public Guid? ContentFileId { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
}
