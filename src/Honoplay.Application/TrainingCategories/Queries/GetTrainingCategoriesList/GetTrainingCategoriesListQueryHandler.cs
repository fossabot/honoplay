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

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList
{
    public class GetTrainingCategoriesListQueryHandler : IRequestHandler<GetTrainingCategoriesListQuery, ResponseModel<TrainingCategoriesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingCategoriesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingCategoriesListModel>> Handle(GetTrainingCategoriesListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = "TrainingCategories";
            var trainingCategoriesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainingCategories
                    .AsNoTracking()
                , cancellationToken);

            if (!trainingCategoriesQueryable.Any())
            {
                throw new NotFoundException();
            }

            var trainingCategoriesList = await trainingCategoriesQueryable
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(TrainingCategoriesListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<TrainingCategoriesListModel>(numberOfTotalItems: trainingCategoriesQueryable.LongCount(), numberOfSkippedItems: request.Skip, source: trainingCategoriesList);

        }
    }
}
