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

namespace Honoplay.Application.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQueryHandler : IRequestHandler<GetTrainingsListQuery, ResponseModel<TrainingsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingsListModel>> Handle(GetTrainingsListQuery request,
            CancellationToken cancellationToken)
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
                .Select(TrainingsListModel.Projection)
                .OrderBy(x => x.Id)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<TrainingsListModel>(numberOfTotalItems: trainingsList.Count, numberOfSkippedItems: request.Skip, source: trainingsList);

        }
    }
}
