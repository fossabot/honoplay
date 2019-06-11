using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQueryHandler : IRequestHandler<GetTrainerDetailQuery, ResponseModel<TrainerDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly IDistributedCache _cache;

        public GetTrainerDetailQueryHandler(HonoplayDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<ResponseModel<TrainerDetailModel>> Handle(GetTrainerDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"TrainersWithDepartmentsByAdminUserId{request.HostName}";
            var serializedRedisTrainers = await _cache.GetStringAsync(redisKey, cancellationToken);
            List<Trainer> redisTrainers;

            if (!string.IsNullOrEmpty(serializedRedisTrainers))
            {
                redisTrainers = JsonConvert.DeserializeObject<List<Trainer>>(serializedRedisTrainers);
            }
            else
            {
                redisTrainers = await _context.Trainers.AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.Id == request.Id &&
                                _context.TenantAdminUsers
                                    .Any(y => y.TenantId == x.Department.TenantId &&
                                              y.AdminUserId == request.AdminUserId))
                    .ToListAsync(cancellationToken);

                if (redisTrainers is null)
                {
                    throw new NotFoundException(nameof(Trainer), request.Id);
                }
                await _cache.SetStringAsync(redisKey, JsonConvert.SerializeObject(redisTrainers), cancellationToken);

            }
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