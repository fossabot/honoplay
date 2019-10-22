using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Tags.Queries.GetTagDetail
{
    public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, ResponseModel<TagDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTagDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TagDetailModel>> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TagsByTenantId{request.TenantId}";
            var redisTags = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Tags
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                , cancellationToken);

            var tag = redisTags.FirstOrDefault(x => x.Id == request.Id);

            if (tag is null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            var model = TagDetailModel.Create(tag);
            return new ResponseModel<TagDetailModel>(model);
        }
    }
}