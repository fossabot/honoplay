using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusCommand : IRequest<ResponseModel<CreateWorkingStatusModel>>
    {
        [JsonIgnore]
        public int CreatedBy { get; set; }
        [JsonIgnore]
        public string HostName { get; set; }
        public string Name { get; set; }
    }
}
