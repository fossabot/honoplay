using System;

namespace Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory
{
    public struct CreateQuestionCategoryModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public CreateQuestionCategoryModel(int id, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
