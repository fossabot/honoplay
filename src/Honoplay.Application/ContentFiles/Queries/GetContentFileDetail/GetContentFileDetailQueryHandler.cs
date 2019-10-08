using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.ContentFiles.Queries.GetContentFileDetail
{
    public class GetContentFileDetailQueryHandler : IRequestHandler<GetContentFileDetailQuery, ResponseModel<ContentFileDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetContentFileDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<ContentFileDetailModel>> Handle(GetContentFileDetailQuery request, CancellationToken cancellationToken)
        {
            var contentFile = await _context.ContentFiles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (contentFile is null)
            {
                throw new NotFoundException(nameof(ContentFile), request.Id);
            }

            var model = ContentFileDetailModel.Create(contentFile);
            return new ResponseModel<ContentFileDetailModel>(model);
        }
    }
}