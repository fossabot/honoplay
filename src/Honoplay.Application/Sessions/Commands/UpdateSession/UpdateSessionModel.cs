using System;

namespace Honoplay.Application.Sessions.Commands.UpdateSession
{
    public struct UpdateSessionModel
    {
        public int Id { get; }
        public int GameId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public int UpdatedBy { get; }
        public DateTimeOffset UpdatedAt { get; }

        public UpdateSessionModel(int id, int gameId, int classroomId, string name, int updatedBy, DateTimeOffset updatedAt)
        {
            Id = id;
            GameId = gameId;
            ClassroomId = classroomId;
            Name = name;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
