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

namespace Honoplay.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQueryHandler : IRequestHandler<GetTagsListQuery, ResponseModel<TagsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTagsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TagsListModel>> Handle(GetTagsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"TagsByTenantId{request.TenantId}";
            var allTagsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionTags
                    .Include(x => x.Question)
                    .Include(x => x.Tag)
                    .Where(x => x.Question.TenantId == request.TenantId)
                    .Select(x => x.Tag)
                , cancellationToken);

            if (!allTagsList.Any())
            {
                throw new NotFoundException();
            }

            var tagsList = allTagsList
                .Select(TagsListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<TagsListModel>(numberOfTotalItems: tagsList.Count, numberOfSkippedItems: request.Skip, source: tagsList);

        }
    }
}
