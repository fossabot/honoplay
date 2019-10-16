using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class QuestionCategory : BaseEntity
    {
        public QuestionCategory()
        {
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public Tenant Tenant { get; set; }
    }
}
