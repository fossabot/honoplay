using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryCommand : IRequest<ResponseModel<CreateQuestionCategoryModel>>
    {
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public string Name { get; set; }
    }
}
