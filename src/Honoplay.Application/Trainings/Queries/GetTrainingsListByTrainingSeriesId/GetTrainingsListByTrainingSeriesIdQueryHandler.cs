using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId
{
    public class GetTrainingsListByTrainingSeriesIdQueryHandler : IRequestHandler<GetTrainingsListByTrainingSeriesIdQuery, ResponseModel<GetTrainingsListByTrainingSeriesIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingsListByTrainingSeriesIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<GetTrainingsListByTrainingSeriesIdModel>> Handle(GetTrainingsListByTrainingSeriesIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingsByTenantId{request.TenantId}";
            var allTrainingsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainings
                    .Include(x => x.TrainingSeries)
                    .AsNoTracking()
                    .Where(x => x.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allTrainingsList.Any())
            {
                throw new NotFoundException();
            }

            var trainingsList = allTrainingsList
                .Where(x => x.TrainingSeriesId == request.Id)
                .Select(GetTrainingsListByTrainingSeriesIdModel.Projection);

            return new ResponseModel<GetTrainingsListByTrainingSeriesIdModel>(numberOfTotalItems: allTrainingsList.LongCount(), numberOfSkippedItems: 0, source: trainingsList);
        }
    }
}