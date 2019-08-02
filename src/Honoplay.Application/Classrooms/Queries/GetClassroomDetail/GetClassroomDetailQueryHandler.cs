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

namespace Honoplay.Application.Classrooms.Queries.GetClassroomDetail
{
    public class GetClassroomDetailQueryHandler : IRequestHandler<GetClassroomDetailQuery, ResponseModel<ClassroomDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetClassroomDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ClassroomDetailModel>> Handle(GetClassroomDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"ClassroomsByTenantId{request.TenantId}";
            var redisClassrooms = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Classrooms
                    .Include(x => x.Training)
                    .Include(x => x.Training.TrainingSeries)
                    .AsNoTracking()
                    .Where(x => x.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            var classroom = redisClassrooms.FirstOrDefault(x => x.Id == request.Id);

            if (classroom is null)
            {
                throw new NotFoundException(nameof(Classroom), request.Id);
            }

            var model = ClassroomDetailModel.Create(classroom);
            return new ResponseModel<ClassroomDetailModel>(model);
        }
    }
}