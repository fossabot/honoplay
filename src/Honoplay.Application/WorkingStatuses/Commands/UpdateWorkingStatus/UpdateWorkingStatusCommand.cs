using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusCommand : IRequest<ResponseModel<UpdateWorkingStatusModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
    }
}
