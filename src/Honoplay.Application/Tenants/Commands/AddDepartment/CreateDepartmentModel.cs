using System;
using System.Collections.Generic;
using Honoplay.Domain.Entities;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public struct CreateDepartmentModel
    {
        public ICollection<Department> Departments { get; set; }


        public CreateDepartmentModel(ICollection<Department> departments)
        {
            Departments = departments;
        }
    }
}