using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
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
            var redisKey = $"DepartmentsByTenantId{request.TenantId}";
            var departmentsList = await _cacheService.RedisCacheAsync<IList<DepartmentsListModel>>(redisKey, delegate
            {
                return _context.Departments
                    .Where(x => x.TenantId == request.TenantId)
                    .AsNoTracking()
                    .Select(DepartmentsListModel.Projection)
                    .ToList();

            }, cancellationToken);

            if (!departmentsList.Any())
            {
                throw new NotFoundException();
            }

            departmentsList = departmentsList
                .OrderBy(x => x.Name)
                .SkipOrAll(request.Skip)
                .TakeOrAll(request.Take)
                .ToList();

            return new ResponseModel<DepartmentsListModel>(numberOfTotalItems: departmentsList.Count, numberOfSkippedItems: request.Skip, source: departmentsList);

        }
    }
}
