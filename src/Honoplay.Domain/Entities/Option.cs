namespace Honoplay.Domain.Entities
{
    public class Option : BaseEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int? VisibilityOrder { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}
