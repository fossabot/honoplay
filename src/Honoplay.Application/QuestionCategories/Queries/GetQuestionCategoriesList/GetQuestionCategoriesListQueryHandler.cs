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

namespace Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public class GetQuestionCategoriesListQueryHandler : IRequestHandler<GetQuestionCategoriesListQuery, ResponseModel<QuestionCategoriesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionCategoriesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionCategoriesListModel>> Handle(GetQuestionCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionCategoriesByTenantId{request.TenantId}";

            var questionCategoriesQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionCategories
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                , cancellationToken);

            if (!questionCategoriesQuery.Any())
            {
                throw new NotFoundException();
            }

            var questionCategoriesList = await questionCategoriesQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .OrderBy(x => x.Id)
                .Select(QuestionCategoriesListModel.Projection)
                .ToListAsync(cancellationToken);

            return new ResponseModel<QuestionCategoriesListModel>(numberOfTotalItems: questionCategoriesQuery.LongCount(),
                                                               numberOfSkippedItems: request.Skip,
                                                               source: questionCategoriesList);
        }
    }
}
