using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
