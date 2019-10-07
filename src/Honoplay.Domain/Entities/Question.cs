using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Options = new HashSet<Option>();
        }
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public Guid? VisualId { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public Tenant Tenant { get; set; }
        public ContentFile ContentFile { get; set; }
    }
}
