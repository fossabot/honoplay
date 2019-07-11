using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQueryHandler : IRequestHandler<GetTraineeDetailQuery, ResponseModel<TraineeDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTraineeDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineeDetailModel>> Handle(GetTraineeDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByTenantId{request.TenantId}";

            var redisTrainees = await _cacheService.RedisCacheAsync<IList<Trainee>>(redisKey, delegate
            {
                return _context.Trainees.AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Id == request.Id
                                && x.Department.TenantId == request.TenantId)
                    .ToList();
            }, cancellationToken);

            var trainee = redisTrainees.FirstOrDefault(x => x.Id == request.Id);

            if (trainee is null)
            {
                throw new NotFoundException(nameof(Trainee), request.Id);
            }

            var model = TraineeDetailModel.Create(trainee);
            return new ResponseModel<TraineeDetailModel>(model);
        }
    }
}