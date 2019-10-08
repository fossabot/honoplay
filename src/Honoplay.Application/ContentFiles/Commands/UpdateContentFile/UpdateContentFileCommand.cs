using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.ContentFiles.Commands.UpdateContentFile
{
    public class UpdateContentFileCommand : IRequest<ResponseModel<UpdateContentFileModel>>
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
