using System;

namespace Honoplay.Application.ContentFiles.Commands.UpdateContentFile
{
    public struct UpdateContentFileModel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int UpdatedBy { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public UpdateContentFileModel(Guid id, string name, string contentType, byte[] data, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            ContentType = contentType;
            Data = data;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
