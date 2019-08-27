using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainerUserId
{
    public class GetTrainingsListByTrainerUserIdQueryHandler : IRequestHandler<GetTrainingsListByTrainerUserIdQuery, ResponseModel<GetTrainingsListByTrainerUserIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingsListByTrainerUserIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<GetTrainingsListByTrainerUserIdModel>> Handle(GetTrainingsListByTrainerUserIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingsByTenantId{request.TenantId}";
            var allTrainingsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainings
                    .Include(x => x.Classrooms)
                    .AsNoTracking()
                    .Where(x => x.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allTrainingsList.Any())
            {
                throw new NotFoundException();
            }

            var trainingsList = allTrainingsList.Where(x => 
                    x.Classrooms.All(y => y.TrainerUserId == request.Id))
                .Select(GetTrainingsListByTrainerUserIdModel.Projection);

            return new ResponseModel<GetTrainingsListByTrainerUserIdModel>(numberOfTotalItems: allTrainingsList.LongCount(), numberOfSkippedItems: 0, source: trainingsList);
        }
    }
}