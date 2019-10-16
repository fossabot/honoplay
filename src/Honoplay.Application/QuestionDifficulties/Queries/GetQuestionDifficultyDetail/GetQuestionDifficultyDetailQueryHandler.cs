using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public class GetQuestionDifficultyDetailQueryHandler : IRequestHandler<GetQuestionDifficultyDetailQuery, ResponseModel<QuestionDifficultyDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionDifficultyDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionDifficultyDetailModel>> Handle(GetQuestionDifficultyDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = "QuestionDifficulties";
            var questionDifficultiesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionDifficulties
                    .AsNoTracking()
                , cancellationToken);

            var questionDifficulty = await questionDifficultiesQueryable
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (questionDifficulty is null)
            {
                throw new NotFoundException(nameof(QuestionDifficulty), request.Id);
            }

            var model = QuestionDifficultyDetailModel.Create(questionDifficulty);
            return new ResponseModel<QuestionDifficultyDetailModel>(model);
        }
    }
}