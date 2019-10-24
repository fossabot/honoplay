using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Avatars.Queries.GetAvatarsList
{
    public class GetAvatarsListQueryHandler : IRequestHandler<GetAvatarsListQuery, ResponseModel<AvatarsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetAvatarsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<AvatarsListModel>> Handle(GetAvatarsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = "Avatars";
            var avatarsQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Avatars
                    .AsNoTracking()
                , cancellationToken);

            if (!avatarsQuery.Any())
            {
                throw new NotFoundException();
            }

            var avatarsList = await avatarsQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(AvatarsListModel.Projection)
                .ToListAsync(cancellationToken);

            return new ResponseModel<AvatarsListModel>(numberOfTotalItems: avatarsQuery.LongCount(), numberOfSkippedItems: request.Skip, source: avatarsList);

        }
    }
}
