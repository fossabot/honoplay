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

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail {
    public class GetTrainerDetailQueryHandler : IRequestHandler<GetTrainerDetailQuery, ResponseModel<TrainerDetailModel>> {
        private readonly HonoplayDbContext _context;
        private readonly IDistributedCache _cache;

        public GetTrainerDetailQueryHandler (HonoplayDbContext context, IDistributedCache cache) {
            _context = context;
            _cache = cache;
        }

        public async Task<ResponseModel<TrainerDetailModel>> Handle (GetTrainerDetailQuery request, CancellationToken cancellationToken) {
            var cacheKey = request.HostName + request.AdminUserId;
            var cachedData = await _cache.GetStringAsync (cacheKey, cancellationToken);

            Trainer trainer;

            if (string.IsNullOrEmpty (cachedData)) {
                var cacheCurrentTenant = await _context.Tenants.Include (x => x.Departments)
                    .FirstOrDefaultAsync (x =>
                        x.HostName == request.HostName &&
                        x.TenantAdminUsers.Any (y =>
                            y.AdminUserId == request.AdminUserId),
                        cancellationToken);

                if (cacheCurrentTenant is null) {
                    throw new NotFoundException (nameof (Tenant), request.HostName);

                }
                await _cache.SetStringAsync (key: cacheKey, value: JsonConvert.SerializeObject (cacheCurrentTenant), cancellationToken);

                trainer = await _context.Trainers.Include (x => x.Department)
                    .Where (x => x.Id == request.Id &&
                        _context.TenantAdminUsers
                        .Any (y => y.TenantId == x.Department.TenantId &&
                            y.AdminUserId == request.AdminUserId))
                    .FirstOrDefaultAsync (cancellationToken);

            } else {
                trainer = JsonConvert.DeserializeObject<Trainer> (cachedData);
            }

            if (trainer is null) {
                throw new NotFoundException (nameof (Trainer), request.Id);
            }

            var model = TrainerDetailModel.Create (trainer);
            return new ResponseModel<TrainerDetailModel> (model);
        }
    }
}