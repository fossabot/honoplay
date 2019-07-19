using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Answers.Queries.GetAnswerDetail
{
    public class GetAnswerDetailQueryHandler : IRequestHandler<GetAnswerDetailQuery, ResponseModel<AnswerDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetAnswerDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<AnswerDetailModel>> Handle(GetAnswerDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"AnswersTenantId{request.TenantId}";

            var redisAnswers = await _cacheService.RedisCacheAsync<IList<Answer>>(redisKey, delegate
            {
                return _context.Answers.AsNoTracking()
                    .Include(x => x.Question)
                    .Where(x => x.Question.TenantId == request.TenantId)
                    .ToList();
            }, cancellationToken);

            var answer = redisAnswers.FirstOrDefault(x => x.Id == request.Id);

            if (answer is null)
            {
                throw new NotFoundException(nameof(Answer), request.Id);
            }

            var model = AnswerDetailModel.Create(answer);
            return new ResponseModel<AnswerDetailModel>(model);
        }
    }
}