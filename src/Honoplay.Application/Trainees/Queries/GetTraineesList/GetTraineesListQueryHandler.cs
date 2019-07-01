using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListQueryHandler : IRequestHandler<GetTraineesListQuery, ResponseModel<TraineesListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;
        public GetTraineesListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<TraineesListModel>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TraineesWithDepartmentsByHostName{request.HostName}";

            var query = await _cacheService.RedisCacheAsync<IList<TraineesListModel>>(redisKey, delegate
            {
                var currentTenant = _context.Tenants.FirstOrDefaultAsync(x =>
                    x.HostName == request.HostName,
                    cancellationToken);

                var isExist = _context.TenantAdminUsers.AnyAsync(x =>
                        x.AdminUserId == request.AdminUserId
                        && x.TenantId == currentTenant.Result.Id,
                    cancellationToken);

                return _context.Trainees.Where(x => isExist.Result)
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .Select(TraineesListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!query.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineesListModel>(numberOfTotalItems: query.Count, numberOfSkippedItems: request.Take, source: query);


        }
    }
}
