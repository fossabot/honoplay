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

namespace Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList
{
    public class GetQuestionTypesListQueryHandler : IRequestHandler<GetQuestionTypesListQuery, ResponseModel<QuestionTypesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetQuestionTypesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<QuestionTypesListModel>> Handle(GetQuestionTypesListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = "QuestionTypes";
            var questionTypesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.QuestionTypes
                    .AsNoTracking()
                , cancellationToken);

            if (!questionTypesQueryable.Any())
            {
                throw new NotFoundException();
            }

            var questionTypesList = await questionTypesQueryable
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(QuestionTypesListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<QuestionTypesListModel>(numberOfTotalItems: questionTypesQueryable.LongCount(), numberOfSkippedItems: request.Skip, source: questionTypesList);

        }
    }
}
