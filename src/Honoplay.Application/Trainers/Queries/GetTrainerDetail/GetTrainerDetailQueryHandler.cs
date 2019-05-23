using Honoplay.Application._Exceptions;
using Honoplay.Application._Infrastructure;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQueryHandler : IRequestHandler<GetTrainerDetailQuery, ResponseModel<TrainerDetailModel>>
    {
        private readonly HonoplayDbContext _context;

        public GetTrainerDetailQueryHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<TrainerDetailModel>> Handle(GetTrainerDetailQuery request, CancellationToken cancellationToken)
        {
            var trainer = await _context.Trainers.Include(x => x.Department)
                .Where(x => x.Id == request.Id &&
                            _context.TenantAdminUsers
                                .Any(y => y.TenantId == x.Department.TenantId &&
                                          y.AdminUserId == request.AdminUserId))
                .FirstOrDefaultAsync(cancellationToken);


            if (trainer is null)
            {
                throw new NotFoundException(nameof(Trainer), request.Id);
            }

            var model = TrainerDetailModel.Create(trainer);
            return new ResponseModel<TrainerDetailModel>(model);
        }
    }
}
