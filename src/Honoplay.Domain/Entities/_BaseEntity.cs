using System;

namespace Honoplay.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTimeOffset.Now;
        }

        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}