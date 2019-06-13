using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQueryHandler : IRequestHandler<GetTrainersListQuery, ResponseModel<TrainersListModel>>
    {

        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetTrainersListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<ResponseModel<TrainersListModel>> Handle(GetTrainersListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByHostName{request.HostName}";
            var query = await _cacheService.RedisCacheAsync<IList<TrainersListModel>>(redisKey, delegate
            {
                var currentTenant = _context.Tenants.FirstOrDefaultAsync(x =>
                        x.HostName == request.HostName,
                    cancellationToken);

                var isExist = _context.TenantAdminUsers.AnyAsync(x =>
                        x.AdminUserId == request.AdminUserId
                        && x.TenantId == currentTenant.Result.Id,
                    cancellationToken);

                return _context.Trainers.Where(x => isExist.Result)
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .Select(TrainersListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!query.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TrainersListModel>(numberOfTotalItems: query.Count, numberOfSkippedItems: request.Take, source: query);
        }
    }
}