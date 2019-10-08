using System;

namespace Honoplay.Application.ContentFiles.Commands.CreateContentFile
{
    public struct CreateContentFileModel
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateContentFileModel(Guid id, string name, string contentType, byte[] data, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            ContentType = contentType;
            Data = data;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }

    }
}
