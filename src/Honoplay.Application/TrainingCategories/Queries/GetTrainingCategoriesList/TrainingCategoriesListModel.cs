using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList
{
    public struct TrainingCategoriesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<TrainingCategory, TrainingCategoriesListModel>> Projection
        {
            get
            {
                return trainingCategory => new TrainingCategoriesListModel
                {
                    Id = trainingCategory.Id,
                    Name = trainingCategory.Name
                };
            }
        }
        public static TrainingCategoriesListModel Create(TrainingCategory trainingCategory)
        {
            return Projection.Compile().Invoke(trainingCategory);
        }
    }
}