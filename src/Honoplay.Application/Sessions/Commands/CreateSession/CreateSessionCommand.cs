using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommand : IRequest<ResponseModel<List<CreateSessionModel>>>
    {
        public List<CreateSessionCommandModel> CreateSessionModels { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
    }
    public class CreateSessionCommandModel
    {
        public int GameId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
    }
}
