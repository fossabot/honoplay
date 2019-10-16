using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryCommand : IRequest<ResponseModel<List<CreateQuestionCategoryModel>>>
    {
        public List<CreateQuestionCategoryCommandModel> CreateQuestionCategoryModels { get; set; }

        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateQuestionCategoryCommandModel
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}