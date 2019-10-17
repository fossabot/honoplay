using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory
{
    public class UpdateQuestionCategoryCommand : IRequest<ResponseModel<UpdateQuestionCategoryModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
    }
}
