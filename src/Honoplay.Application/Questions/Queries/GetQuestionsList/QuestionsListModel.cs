﻿using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Questions.Queries.GetQuestionsList
{
    public struct QuestionsListModel
    {
        public int Id { get; private set; }
        public string Text { get; set; }
        public int Duration { get; set; }
        public int? DifficultyId { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }
        public Guid? ContentFileId { get; set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public static Expression<Func<Question, QuestionsListModel>> Projection => question => new QuestionsListModel
        {
            Id = question.Id,
            DifficultyId = question.QuestionDifficultyId,
            ContentFileId = question.ContentFileId,
            CategoryId = question.QuestionCategoryId,
            TypeId = question.QuestionTypeId,
            CreatedBy = question.CreatedBy,
            UpdatedBy = question.UpdatedBy,
            UpdatedAt = question.UpdatedAt,
            CreatedAt = question.CreatedAt,
            Text = question.Text,
            Duration = question.Duration
        };

        public static QuestionsListModel Create(Question question) => Projection.Compile().Invoke(question);
    }
}
