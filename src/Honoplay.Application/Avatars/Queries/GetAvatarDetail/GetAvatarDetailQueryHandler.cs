using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Avatars.Queries.GetAvatarDetail
{
    public class GetAvatarDetailQueryHandler : IRequestHandler<GetAvatarDetailQuery, ResponseModel<AvatarDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetAvatarDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<AvatarDetailModel>> Handle(GetAvatarDetailQuery request, CancellationToken cancellationToken)
        {
            var avatar = await _context.Avatars
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (avatar is null)
            {
                throw new NotFoundException(nameof(Avatar), request.Id);
            }

            var model = AvatarDetailModel.Create(avatar);
            return new ResponseModel<AvatarDetailModel>(model);
        }
    }
}