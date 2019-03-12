using System.Collections.Generic;



namespace Honoplay.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public Tenant()
        {
            //Default values for non nullable refs.
            Name = HostName = "";

            TenantAdminUsers = new HashSet<TenantAdminUser>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public string HostName { get; set; }

        public byte[] Logo { get; set; }

        public virtual ICollection<TenantAdminUser> TenantAdminUsers { get; set; }
    }
}