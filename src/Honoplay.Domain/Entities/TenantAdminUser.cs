using System;

namespace Honoplay.Domain.Entities
{
    public class TenantAdminUser : BaseEntity
    {
        public int Id { get; set; }

        public Guid TenantId { get; set; }
        public int AdminUserId { get; set; }

        public Tenant Tenant { get; set; }
        public AdminUser AdminUser { get; set; }
    }
}