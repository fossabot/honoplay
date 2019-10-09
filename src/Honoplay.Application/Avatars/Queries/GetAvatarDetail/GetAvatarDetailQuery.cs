using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Avatars.Queries.GetAvatarDetail
{
    public class GetAvatarDetailQuery : IRequest<ResponseModel<AvatarDetailModel>>
    {
        public GetAvatarDetailQuery(int id)
        {
            Id = id;
        }

        public GetAvatarDetailQuery() { }

        public int Id { get; private set; }
    }
}
