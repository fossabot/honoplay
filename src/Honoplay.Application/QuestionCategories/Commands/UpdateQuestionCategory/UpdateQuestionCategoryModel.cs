using System;

namespace Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory
{
    public struct UpdateQuestionCategoryModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateQuestionCategoryModel(int id, string name, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
