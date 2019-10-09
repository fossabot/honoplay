using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Avatars.Queries.GetAvatarsList
{
    public class GetAvatarsListQuery : IRequest<ResponseModel<AvatarsListModel>>
    {
        public GetAvatarsListQuery(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }

        public int? Skip { get; private set; }
        public int? Take { get; private set; }

    }
    public class GetAvatarsListQueryModel : IRequest<ResponseModel<AvatarsListModel>>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
