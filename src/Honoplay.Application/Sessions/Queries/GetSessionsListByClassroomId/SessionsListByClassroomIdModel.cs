﻿using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId
{
    public struct SessionsListByClassroomIdModel
    {
        public int Id { get; private set; }
        public int ClassroomId { get; private set; }
        public int GameId { get; private set; }
        public string Name { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Expression<Func<Session, SessionsListByClassroomIdModel>> Projection
        {
            get
            {
                return session => new SessionsListByClassroomIdModel
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
        public static SessionsListByClassroomIdModel Create(Session session)
        {
            return Projection.Compile().Invoke(session);
        }
    }
}
