﻿using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainerUserId
{
    public struct GetTrainingsListByTrainerUserIdModel
    {
        public int Id { get; set; }
        public int TrainingSeriesId { get; set; }
        public int TrainingCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset BeginDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }

        public static Expression<Func<Training, GetTrainingsListByTrainerUserIdModel>> Projection
        {
            get
            {
                return training => new GetTrainingsListByTrainerUserIdModel
                {
                    Id = training.Id,
                    Name = training.Name,
                    Description = training.Description,
                    TrainingSeriesId = training.TrainingSeriesId,
                    TrainingCategoryId = training.TrainingCategoryId,
                    CreatedBy = training.CreatedBy,
                    CreatedAt = training.CreatedAt,
                    UpdatedBy = training.UpdatedBy,
                    UpdatedAt = training.UpdatedAt,
                    BeginDateTime = training.BeginDateTime,
                    EndDateTime = training.EndDateTime
                };
            }
        }
        public static GetTrainingsListByTrainerUserIdModel Create(Training training)
        {
            return Projection.Compile().Invoke(training);
        }
    }
}
