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

namespace Honoplay.Application.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailQueryHandler : IRequestHandler<GetTrainingDetailQuery, ResponseModel<TrainingDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainingDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainingDetailModel>> Handle(GetTrainingDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainingsByTenantId{request.TenantId}";
            var redisTrainings = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainings
                    .Include(x => x.TrainingSeries)
                    .AsNoTracking()
                    .Where(x => x.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            var training = redisTrainings.FirstOrDefault(x => x.Id == request.Id);

            if (training is null)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }

            var model = TrainingDetailModel.Create(training);
            return new ResponseModel<TrainingDetailModel>(model);
        }
    }
}