using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Sessions.Queries.GetSessionsList
{
    public struct SessionsListModel
    {
        public int Id { get; private set; }
        public int ClassroomId { get; private set; }
        public int GameId { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<Session, SessionsListModel>> Projection
        {
            get
            {
                return session => new SessionsListModel
                {
                    Id = session.Id,
                    Name = session.Name,
                    ClassroomId = session.ClassroomId,
                    GameId = session.GameId,
                    CreatedBy = session.CreatedBy,
                    CreatedAt = session.CreatedAt,
                    UpdatedBy = session.UpdatedBy,
                    UpdatedAt = session.UpdatedAt,
                };
            }
        }
        public static SessionsListModel Create(Session session)
        {
            return Projection.Compile().Invoke(session);
        }
    }
}
