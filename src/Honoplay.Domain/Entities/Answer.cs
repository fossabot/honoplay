namespace Honoplay.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int OrderBy { get; set; }
        public Question Question { get; set; }
    }
}
