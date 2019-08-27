using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Classrooms.Queries.GetClassroomsListByTrainingIdWithTrainerUserId
{
    public class GetClassroomsListByTrainingIdWithTrainerUserIdQueryHandler : IRequestHandler<GetClassroomsListByTrainingIdWithTrainerUserIdQuery, ResponseModel<ClassroomsListByTrainingIdWithTrainerUserIdModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetClassroomsListByTrainingIdWithTrainerUserIdQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ClassroomsListByTrainingIdWithTrainerUserIdModel>> Handle(GetClassroomsListByTrainingIdWithTrainerUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var redisKey = $"ClassroomsByTenantId{request.TenantId}";
            var allClassroomsList = await _cacheService.RedisCacheAsync(redisKey,
                _ => _context.Classrooms
                    .Include(x => x.Training)
                    .Include(x => x.Training.TrainingSeries)
                    .AsNoTracking()
                    .Where(x => x.Training.TrainingSeries.TenantId == request.TenantId)
                , cancellationToken);

            if (!allClassroomsList.Any())
            {
                throw new NotFoundException();
            }

            var classroomsList = allClassroomsList
                .Where(x => x.TrainingId == request.TrainingId
                            && x.TrainerUserId == request.TrainerUserId)
                .Select(ClassroomsListByTrainingIdWithTrainerUserIdModel.Projection)
                .ToList();

            return new ResponseModel<ClassroomsListByTrainingIdWithTrainerUserIdModel>(numberOfTotalItems: classroomsList.Count, numberOfSkippedItems: 0, source: classroomsList);

        }
    }
}
