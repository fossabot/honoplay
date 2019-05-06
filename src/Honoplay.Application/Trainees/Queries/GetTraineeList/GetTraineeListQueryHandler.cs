using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainees.Queries.GetTraineeList
{
    public class GetTraineeListQueryHandler : IRequestHandler<GetTraineeListQuery, ResponseModel<TraineeListModel>>
    {
        private readonly HonoplayDbContext _context;
        public GetTraineeListQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TraineeListModel>> Handle(GetTraineeListQuery request, CancellationToken cancellationToken)
        {

            var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                x.AdminUserId == request.AdminUserId, cancellationToken);

            var query = _context.Trainees.Where(x => isExist).AsNoTracking().OrderBy(x => x.Name);
            var result = query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(TraineeListModel.Projection)
                .ToList();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineeListModel>(numberOfTotalItems: await query.CountAsync(cancellationToken), numberOfSkippedItems: request.Take, source: result);


        }
    }
}
