using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public struct QuestionCategoriesListModel
    {
        public int Id { get; private set; }
        public Guid TenantId { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<QuestionCategory, QuestionCategoriesListModel>> Projection
        {
            get
            {
                return questionCategory => new QuestionCategoriesListModel
                {
                    Id = questionCategory.Id,
                    Name = questionCategory.Name,
                    TenantId = questionCategory.TenantId,
                    CreatedBy = questionCategory.CreatedBy,
                    CreatedAt = questionCategory.CreatedAt,
                    UpdatedBy = questionCategory.UpdatedBy,
                    UpdatedAt = questionCategory.UpdatedAt
                };
            }
        }

        public static QuestionCategoriesListModel Create(QuestionCategory questionCategory)
        {
            return Projection.Compile().Invoke(questionCategory);
        }
    }
}
