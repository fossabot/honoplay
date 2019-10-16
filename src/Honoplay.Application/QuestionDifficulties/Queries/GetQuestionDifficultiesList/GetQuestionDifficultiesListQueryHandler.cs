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

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList
{
    public class GetQuestionDifficultiesListQueryHandler : IRequestHandler<GetQuestionDifficultiesListQuery, ResponseModel<QuestionDifficultiesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionDifficultiesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionDifficultiesListModel>> Handle(GetQuestionDifficultiesListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = "QuestionDifficulties";
            var questionDifficultiesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionDifficulties
                    .AsNoTracking()
                , cancellationToken);

            if (!questionDifficultiesQueryable.Any())
            {
                throw new NotFoundException();
            }

            var questionDifficultiesList = await questionDifficultiesQueryable
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(QuestionDifficultiesListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<QuestionDifficultiesListModel>(numberOfTotalItems: questionDifficultiesQueryable.LongCount(), numberOfSkippedItems: request.Skip, source: questionDifficultiesList);

        }
    }
}
