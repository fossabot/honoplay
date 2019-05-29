using Honoplay.Application._Infrastructure;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public class CreateDepartmentCommand : IRequest<ResponseModel<CreateDepartmentModel>>
    {
        [JsonIgnore]
        public int AdminUserId { get; set; }
        public string HostName { get; set; }
        public ICollection<string> Departments { get; set; }
    }
}
