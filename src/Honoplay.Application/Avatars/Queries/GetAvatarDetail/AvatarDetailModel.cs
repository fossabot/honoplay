using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Avatars.Queries.GetAvatarDetail
{
    public struct AvatarDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageBytes { get; set; }

        public static Expression<Func<Avatar, AvatarDetailModel>> Projection
        {
            get
            {
                return avatar => new AvatarDetailModel
                {
                    Id = avatar.Id,
                    Name = avatar.Name,
                    ImageBytes = avatar.ImageBytes,
                };
            }
        }
        public static AvatarDetailModel Create(Avatar avatar)
        {
            return Projection.Compile().Invoke(avatar);
        }
    }
}
