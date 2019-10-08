using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFilesList
{
    public struct ContentFilesListModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<ContentFile, ContentFilesListModel>> Projection
        {
            get
            {
                return contentFile => new ContentFilesListModel
                {
                    Id = contentFile.Id,
                    Name = contentFile.Name,
                    ContentType = contentFile.ContentType,
                    Data = contentFile.Data,
                    CreatedBy = contentFile.CreatedBy,
                    UpdatedBy = contentFile.UpdatedBy,
                    UpdatedAt = contentFile.UpdatedAt,
                    CreatedAt = contentFile.CreatedAt
                };
            }
        }
        public static ContentFilesListModel Create(ContentFile contentFile)
        {
            return Projection.Compile().Invoke(contentFile);
        }
    }
}
