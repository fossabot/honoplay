using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFileDetail
{
    public struct ContentFileDetailModel
    {
        public Guid Id { get; private set; }
        public string ContentType { get; private set; }
        public byte[] Data { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<ContentFile, ContentFileDetailModel>> Projection
        {
            get
            {
                return contentFile => new ContentFileDetailModel
                {
                    Id = contentFile.Id,
                    Name = contentFile.Name,
                    ContentType = contentFile.ContentType,
                    Data = contentFile.Data,
                    CreatedBy = contentFile.CreatedBy,
                    CreatedAt = contentFile.CreatedAt,
                    UpdatedBy = contentFile.UpdatedBy,
                    UpdatedAt = contentFile.UpdatedAt,
                };
            }
        }
        public static ContentFileDetailModel Create(ContentFile contentFile)
        {
            return Projection.Compile().Invoke(contentFile);
        }
    }
}
