using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Professions.Commands.CreateProfession
{
    public class CreateProfessionCommand : IRequest<ResponseModel<CreateProfessionModel>>
    {
        [JsonIgnore]
        public int AdminUserId { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public ICollection<string> Professions { get; set; }
    }
}
