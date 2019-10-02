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

namespace Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public class GetTraineeGroupDetailQueryHandler : IRequestHandler<GetTraineeGroupDetailQuery, ResponseModel<TraineeGroupDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeGroupDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeGroupDetailModel>> Handle(GetTraineeGroupDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineeGroupsByTenantId{request.TenantId}";

            var redisTraineeGroups = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.TraineeGroups
                    .AsNoTracking()
                    .Where(x => x.TenantId == request.TenantId)
                    .ToList()
                , cancellationToken);

            var traineeGroup = redisTraineeGroups.FirstOrDefault(x => x.Id == request.Id);

            if (traineeGroup is null)
            {
                throw new NotFoundException(nameof(TraineeGroup), request.Id);
            }

            var model = TraineeGroupDetailModel.Create(traineeGroup);
            return new ResponseModel<TraineeGroupDetailModel>(model);
        }
    }
}
