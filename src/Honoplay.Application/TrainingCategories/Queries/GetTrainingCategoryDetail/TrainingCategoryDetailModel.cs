using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public struct TrainingCategoryDetailModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public static Expression<Func<TrainingCategory, TrainingCategoryDetailModel>> Projection
        {
            get
            {
                return trainingCategory => new TrainingCategoryDetailModel
                {
                    Id = trainingCategory.Id,
                    Name = trainingCategory.Name
                };
            }
        }
        public static TrainingCategoryDetailModel Create(TrainingCategory trainingCategory)
        {
            return Projection.Compile().Invoke(trainingCategory);
        }
    }
}
