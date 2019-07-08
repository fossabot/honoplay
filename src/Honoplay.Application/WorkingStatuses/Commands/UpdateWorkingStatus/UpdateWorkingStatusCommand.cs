using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusCommand : IRequest<ResponseModel<UpdateWorkingStatusModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string HostName { get; set; }
        [JsonIgnore]
        public int UpdatedBy { get; set; }
    }
}
