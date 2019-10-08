using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.ContentFiles.Commands.CreateContentFile
{
    public class CreateContentFileCommand : IRequest<ResponseModel<List<CreateContentFileModel>>>
    {
        public List<CreateContentFileCommandModel> CreateContentFileModels { get; set; }

        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateContentFileCommandModel
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
