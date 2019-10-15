using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Tags.Queries.GetTagsListByQuestionId
{
    public struct TagsListByQuestionIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ToQuestion { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Tag, TagsListByQuestionIdModel>> Projection
        {
            get
            {
                return tag => new TagsListByQuestionIdModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    ToQuestion = tag.ToQuestion,
                    CreatedBy = tag.CreatedBy,
                    UpdatedBy = tag.UpdatedBy,
                    UpdatedAt = tag.UpdatedAt,
                    CreatedAt = tag.CreatedAt
                };
            }
        }
        public static TagsListByQuestionIdModel Create(Tag tag)
        {
            return Projection.Compile().Invoke(tag);
        }
    }
}
