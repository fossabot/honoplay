using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public Tenant Tenant { get; set; }
    }
}
