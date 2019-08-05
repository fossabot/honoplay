using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommand : IRequest<ResponseModel<UpdateSessionModel>>
    {
        [JsonIgnore]
        public int UpdatedBy { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public int Id { get; set; }
        public int GameId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
    }
}
