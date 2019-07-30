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

namespace Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesDetail
{
    public class GetTrainingSeriesDetailQueryHandler : IRequestHandler<GetTrainingSeriesDetailQuery, ResponseModel<TrainingSeriesDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingSeriesDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingSeriesDetailModel>> Handle(GetTrainingSeriesDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingSeriesesWithQuestionByTenantId{request.TenantId}";
            var redisTrainingSerieses = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TrainingSerieses
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                , cancellationToken);

            var trainingSeries = redisTrainingSerieses.FirstOrDefault(x => x.Id == request.Id);

            if (trainingSeries is null)
            {
                throw new NotFoundException(nameof(TrainingSeries), request.Id);
            }

            var model = TrainingSeriesDetailModel.Create(trainingSeries);
            return new ResponseModel<TrainingSeriesDetailModel>(model);
        }
    }
}