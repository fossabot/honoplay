using System;

namespace Honoplay.Application.Sessions.Commands.CreateSession
{
    public struct CreateSessionModel
    {
        public int Id { get; }
        public int GameId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; }

        public CreateSessionModel(int id, int gameId, int classroomId, string name, int createdBy, DateTimeOffset createdAt)
        {
            Id = id;
            GameId = gameId;
            ClassroomId = classroomId;
            Name = name;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
