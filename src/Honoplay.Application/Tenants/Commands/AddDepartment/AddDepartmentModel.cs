using System;
using System.Collections.Generic;
using Honoplay.Domain.Entities;

namespace Honoplay.Application.Tenants.Commands.AddDepartment
{
    public struct AddDepartmentModel
    {
        public ICollection<Department> Departments { get; set; }


        public AddDepartmentModel(ICollection<Department> departments)
        {
            Departments = departments;
        }
    }
}