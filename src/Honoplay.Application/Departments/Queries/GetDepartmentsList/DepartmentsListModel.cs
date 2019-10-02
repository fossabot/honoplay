using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Departments.Queries.GetDepartmentsList
{
    public struct DepartmentsListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Department, DepartmentsListModel>> Projection
        {
            get
            {
                return department => new DepartmentsListModel
                {
                    Id = department.Id,
                    Name = department.Name,
                    TenantId = department.TenantId,
                    CreatedBy = department.CreatedBy,
                    UpdatedBy = department.UpdatedBy,
                    UpdatedAt = department.UpdatedAt,
                    CreatedAt = department.CreatedAt
                };
            }
        }
        public static DepartmentsListModel Create(Department department)
        {
            return Projection.Compile().Invoke(department);
        }
    }
}
