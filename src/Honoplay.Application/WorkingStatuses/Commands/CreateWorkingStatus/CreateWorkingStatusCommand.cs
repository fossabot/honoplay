using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusCommand : IRequest<ResponseModel<CreateWorkingStatusModel>>
    {
        public int CreatedBy { get; set; }
        public string HostName { get; set; }
        public string Name { get; set; }
    }
}
