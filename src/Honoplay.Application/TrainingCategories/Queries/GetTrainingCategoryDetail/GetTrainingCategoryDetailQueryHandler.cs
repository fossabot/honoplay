using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public class GetTrainingCategoryDetailQueryHandler : IRequestHandler<GetTrainingCategoryDetailQuery, ResponseModel<TrainingCategoryDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingCategoryDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingCategoryDetailModel>> Handle(GetTrainingCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = "TrainingCategories";
            var trainingCategoriesQueryable = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainingCategories
                    .AsNoTracking()
                , cancellationToken);

            var trainingCategory = await trainingCategoriesQueryable
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (trainingCategory is null)
            {
                throw new NotFoundException(nameof(TrainingCategory), request.Id);
            }

            var model = TrainingCategoryDetailModel.Create(trainingCategory);
            return new ResponseModel<TrainingCategoryDetailModel>(model);
        }
    }
}