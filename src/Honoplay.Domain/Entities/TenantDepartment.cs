using System;

namespace Honoplay.Domain.Entities
{
    public class TenantDepartment
    {
        public Guid TenantId { get; set; }
        public int DepartmentId { get; set; }

        public Tenant Tenant { get; set; }
        public Department Department { get; set; }
    }
}
