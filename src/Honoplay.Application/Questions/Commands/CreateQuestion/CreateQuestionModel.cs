using System;

namespace Honoplay.Application.Questions.Commands.CreateQuestion
{
    public struct CreateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? DifficultyId { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public Guid? ContentFileId { get; set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateQuestionModel(int id, string text, int duration, int? difficultyId, int? categoryId, int? typeId, Guid? contentFileId, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            DifficultyId = difficultyId;
            CategoryId = categoryId;
            TypeId = typeId;
            ContentFileId = contentFileId;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
