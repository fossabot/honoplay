using System;

namespace Honoplay.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}