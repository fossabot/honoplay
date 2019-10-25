using Honoplay.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Honoplay.Application.Questions.Commands.UpdateQuestion
{
    public struct UpdateQuestionModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? DifficultyId { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Guid? ContentFileId { get; set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateQuestionModel(int id, string text, int duration, int? difficultyId, int? categoryId, int? typeId, ICollection<Tag> tags, Guid? contentFileId, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Text = text;
            Duration = duration;
            DifficultyId = difficultyId;
            CategoryId = categoryId;
            TypeId = typeId;
            Tags = tags;
            ContentFileId = contentFileId;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
