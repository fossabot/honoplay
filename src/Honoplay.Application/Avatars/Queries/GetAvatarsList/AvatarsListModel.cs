using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Avatars.Queries.GetAvatarsList
{
    public struct AvatarsListModel
    {
        public int Id { get; set; }

        public static Expression<Func<Avatar, AvatarsListModel>> Projection => avatar => new AvatarsListModel
        {
            Id = avatar.Id
        };
        public static AvatarsListModel Create(Avatar avatar) => Projection.Compile().Invoke(avatar);
    }
}
