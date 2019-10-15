using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Tags.Queries.GetTagDetail
{
    public struct TagDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ToQuestion { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        private static Expression<Func<Tag, TagDetailModel>> Projection
        {
            get
            {
                return tag => new TagDetailModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    ToQuestion = tag.ToQuestion,
                    UpdatedBy = tag.UpdatedBy,
                    UpdatedAt = tag.UpdatedAt,
                    CreatedBy = tag.CreatedBy,
                    CreatedAt = tag.CreatedAt
                };
            }
        }

        public static TagDetailModel Create(Tag tag)
        {
            return Projection.Compile().Invoke(tag);
        }
    }
}
