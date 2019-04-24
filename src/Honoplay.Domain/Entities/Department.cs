using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Domain.Entities
{
    public class Department
    {
        public Department()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TenantDepartment> TenantDepartments { get; set; }
    }
}
