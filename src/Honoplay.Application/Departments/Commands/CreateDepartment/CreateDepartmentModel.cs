using Honoplay.Domain.Entities;
using System.Collections.Generic;

namespace Honoplay.Application.Departments.Commands.CreateDepartment
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