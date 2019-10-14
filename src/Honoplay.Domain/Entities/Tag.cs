using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            QuestionTags = new HashSet<QuestionTag>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
    }
}
