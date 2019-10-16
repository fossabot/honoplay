using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailQueryHandler : IRequestHandler<GetQuestionTypeDetailQuery, ResponseModel<QuestionTypeDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionTypeDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionTypeDetailModel>> Handle(GetQuestionTypeDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = "QuestionTypes";
            var questionTypesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionTypes
                    .AsNoTracking()
                , cancellationToken);

            var questionType = await questionTypesQueryable
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (questionType is null)
            {
                throw new NotFoundException(nameof(QuestionType), request.Id);
            }

            var model = QuestionTypeDetailModel.Create(questionType);
            return new ResponseModel<QuestionTypeDetailModel>(model);
        }
    }
}