using Honoplay.Application._Infrastructure;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Tags.Queries.GetTagsListByQuestionId
{
    public class GetTagsListByQuestionIdQueryHandler : IRequestHandler<GetTagsListByQuestionIdQuery, ResponseModel<TagsListByQuestionIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTagsListByQuestionIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TagsListByQuestionIdModel>> Handle(GetTagsListByQuestionIdQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"TagsByTenantId{request.TenantId}";
            var allTagsListByQuestionId = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionTags
                    .Include(x => x.Question)
                    .Include(x => x.Tag)
                    .Where(x => x.Question.TenantId == request.TenantId)
                , cancellationToken);

            var tagsListByQuestionId = allTagsListByQuestionId
                .Where(x => x.QuestionId == request.QuestionId)
                .Select(x => x.Tag);

            var responseModelSource = await tagsListByQuestionId
                .Select(TagsListByQuestionIdModel.Projection)
                .ToListAsync(cancellationToken);


            return new ResponseModel<TagsListByQuestionIdModel>(numberOfTotalItems: allTagsListByQuestionId.LongCount(), numberOfSkippedItems: 0, source: responseModelSource);

        }
    }
}
