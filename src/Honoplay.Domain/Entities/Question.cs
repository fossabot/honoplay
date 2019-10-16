using System;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class Question : BaseEntity
    {
        public Question()
        {
            Options = new HashSet<Option>();
            QuestionTags = new HashSet<QuestionTag>();
        }
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public Guid? VisualId { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? QuestionDifficultyId { get; set; }
        public int? QuestionCategoryId { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
        public Tenant Tenant { get; set; }
        public ContentFile ContentFile { get; set; }
        public QuestionType QuestionType { get; set; }
        public QuestionCategory QuestionCategory { get; set; }
        public QuestionDifficulty QuestionDifficulty { get; set; }
    }
}
