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

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQueryHandler : IRequestHandler<GetTrainerDetailQuery, ResponseModel<TrainerDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainerDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TrainerDetailModel>> Handle(GetTrainerDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByTenantId{request.TenantId}";

            var redisTrainers = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Trainers
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Department.TenantId == request.TenantId)
                , cancellationToken);

            var trainer = redisTrainers.FirstOrDefault(x => x.Id == request.Id);

            if (trainer is null)
            {
                throw new NotFoundException(nameof(Trainer), request.Id);
            }

            var model = TrainerDetailModel.Create(trainer);
            return new ResponseModel<TrainerDetailModel>(model);
        }
    }
}