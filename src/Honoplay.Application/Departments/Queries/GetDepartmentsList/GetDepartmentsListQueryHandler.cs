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

namespace Honoplay.Application.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListQueryHandler : IRequestHandler<GetDepartmentsListQuery, ResponseModel<DepartmentsListModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetDepartmentsListQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<DepartmentsListModel>> Handle(GetDepartmentsListQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"DepartmentsByHostName{request.HostName}";
            var query = await _cacheService.RedisCacheAsync<IList<DepartmentsListModel>>(redisKey, delegate
            {
                var currentTenant = _context.Tenants.FirstOrDefaultAsync(x =>
                        x.HostName == request.HostName,
                    cancellationToken).Result;

                var isExist = _context.TenantAdminUsers.FirstOrDefaultAsync(x =>
                        x.AdminUserId == request.AdminUserId
                        && x.TenantId == currentTenant.Id,
                    cancellationToken).Result;

                return _context.Departments.Where(x => isExist.TenantId == x.TenantId)
                    .AsNoTracking()
                    .Select(DepartmentsListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!query.Any())
            {
                throw new NotFoundException();
            }

            query = query
                .OrderBy(x => x.Name)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToList();

            return new ResponseModel<DepartmentsListModel>(numberOfTotalItems: query.Count, numberOfSkippedItems: request.Take, source: query);

        }
    }
}
