using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public class AddDepartmentCommand : IRequest<ResponseModel<AddDepartmentModel>>
    {
        [JsonIgnore]
        public int AdminUserId { get; set; }
        public Guid TenantId { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
