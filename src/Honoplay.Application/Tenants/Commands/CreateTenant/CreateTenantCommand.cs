using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<ResponseModel<CreateTenantModel>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }
        [JsonIgnore]
        public int CreatedBy { get; set; }
    }
}