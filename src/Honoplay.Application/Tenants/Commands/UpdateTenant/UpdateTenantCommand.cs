using MediatR;
using System;
using Honoplay.Application._Infrastructure;


namespace Honoplay.Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommand : IRequest<ResponseModel<UpdateTenantModel>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }
        public int UpdatedBy { get; set; }
    }
}