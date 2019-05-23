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
    public class GetTraineesListQueryHandler : IRequestHandler<GetTraineesListQuery, ResponseModel<TraineesListModel>>
    {
        private readonly HonoplayDbContext _context;
        public GetTraineesListQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TraineesListModel>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {

            var isExist = await _context.TenantAdminUsers.AnyAsync(x =>
                x.AdminUserId == request.AdminUserId &&
                x.TenantId == request.TenantId
                , cancellationToken);

            var query = _context.Trainees.Where(x => isExist).AsNoTracking().OrderBy(x => x.Name);
            var result = query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(TraineesListModel.Projection)
                .ToList();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return new ResponseModel<TraineesListModel>(numberOfTotalItems: await query.CountAsync(cancellationToken), numberOfSkippedItems: request.Take, source: result);


        }
    }
}
