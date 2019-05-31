using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Honoplay.Application.Trainers.Queries.GetTrainersList {
    public class GetTrainersListQueryHandler : IRequestHandler<GetTrainersListQuery, ResponseModel<TrainersListModel>> {

        private readonly HonoplayDbContext _context;
        private readonly IDistributedCache _cache;

        public GetTrainersListQueryHandler (HonoplayDbContext context, IDistributedCache cache) {
            _context = context;
            _cache = cache;
        }
        public async Task<ResponseModel<TrainersListModel>> Handle (GetTrainersListQuery request, CancellationToken cancellationToken) {
            var cacheKey = request.HostName + request.AdminUserId;
            var cachedData = await _cache.GetStringAsync (cacheKey, cancellationToken);
            Tenant cacheCurrentTenant;

            if (string.IsNullOrEmpty (cachedData)) {
                cacheCurrentTenant = await _context.Tenants.Include (x => x.Departments)
                    .FirstOrDefaultAsync (x =>
                        x.HostName == request.HostName &&
                        x.TenantAdminUsers.Any (y =>
                            y.AdminUserId == request.AdminUserId),
                        cancellationToken);

                if (cacheCurrentTenant is null) {
                    throw new NotFoundException (nameof (Tenant), request.HostName);

                }
                await _cache.SetStringAsync (key: cacheKey, value: JsonConvert.SerializeObject (cacheCurrentTenant), cancellationToken);
            } else {
                cacheCurrentTenant = JsonConvert.DeserializeObject<Tenant> (cachedData);
            }
            var query = _context.Trainers.Where (x => cacheCurrentTenant.Departments.Any (y => y.Id == x.DepartmentId)).AsNoTracking ().OrderBy (x => x.Name);

            var result = query
                .Skip (request.Skip)
                .Take (request.Take)
                .Select (TrainersListModel.Projection)
                .ToList ();

            if (!result.Any ()) {
                throw new NotFoundException ();
            }

            return new ResponseModel<TrainersListModel> (numberOfTotalItems: await query.CountAsync (cancellationToken), numberOfSkippedItems: request.Take, source: result);
        }
    }
}