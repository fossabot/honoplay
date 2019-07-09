using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<ResponseModel<CreateDepartmentModel>>
    {
        [JsonIgnore]
        public int AdminUserId { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public ICollection<string> Departments { get; set; }
    }
}
