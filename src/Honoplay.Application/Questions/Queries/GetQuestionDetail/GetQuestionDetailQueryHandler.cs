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

namespace Honoplay.Application.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQueryHandler : IRequestHandler<GetQuestionDetailQuery, ResponseModel<QuestionDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionDetailModel>> Handle(GetQuestionDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"QuestionsByTenantId{request.TenantId}";

            var redisQuestions = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Questions
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId && x.Id == request.Id)
                    .Include(y => y.QuestionTags)
                    .ThenInclude(y => y.Tag)
                    .ToList()
                , cancellationToken);

            var question = redisQuestions.FirstOrDefault(x => x.Id == request.Id);

            if (question is null)
            {
                throw new NotFoundException(nameof(Question), request.Id);
            }

            var model = QuestionDetailModel.Create(question);
            return new ResponseModel<QuestionDetailModel>(model);
        }
    }
}
