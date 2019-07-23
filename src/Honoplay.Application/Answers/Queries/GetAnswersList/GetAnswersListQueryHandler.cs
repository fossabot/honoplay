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

namespace Honoplay.Application.Answers.Queries.GetAnswersList
{
    public class GetAnswersListQueryHandler : IRequestHandler<GetAnswersListQuery, ResponseModel<AnswersListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetAnswersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<AnswersListModel>> Handle(GetAnswersListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"AnswersWithQuestionByTenantId{request.TenantId}";
            var answersQuery = await _cacheService.RedisCacheAsync(redisKey, _ => _context.Answers
                    .Include(x => x.Question)
                    .AsNoTracking()
                    .Where(x => x.Question.TenantId == request.TenantId)
                , cancellationToken);

            if (!answersQuery.Any())
            {
                throw new NotFoundException();
            }

            var answersList = await answersQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(AnswersListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<AnswersListModel>(numberOfTotalItems: answersQuery.LongCount(), numberOfSkippedItems: request.Skip, source: answersList);

        }
    }
}
