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
            var redisKey = $"DepartmentsByTenantId{request.TenantId}";
            var departmentsQuery = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Departments
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
            , cancellationToken);

            if (!departmentsQuery.Any())
            {
                throw new NotFoundException();
            }

            var departmentsList = await departmentsQuery
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .Select(DepartmentsListModel.Projection)
                .OrderBy(x => x.Id)
                .ToListAsync(cancellationToken);

            return new ResponseModel<DepartmentsListModel>(numberOfTotalItems: departmentsQuery.LongCount(), numberOfSkippedItems: request.Skip, source: departmentsList);

        }
    }
}
