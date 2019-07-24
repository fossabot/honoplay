using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Options.Queries.GetOptionDetail
{
    public class GetOptionDetailQueryHandler : IRequestHandler<GetOptionDetailQuery, ResponseModel<OptionDetailModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public GetOptionDetailQueryHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<OptionDetailModel>> Handle(GetOptionDetailQuery request, CancellationToken cancellationToken)
        {
            var redisKey = $"OptionsWithQuestionByTenantId{request.TenantId}";
            var redisOptions = await _cacheService.RedisCacheAsync<IQueryable<Option>>(redisKey, delegate
            {
                return _context.Options
                    .Include(x => x.Question)
                    .AsNoTracking()
                    .Where(x => x.Question.TenantId == request.TenantId);
            }, cancellationToken);

            var option = redisOptions.FirstOrDefault(x => x.Id == request.Id);

            if (option is null)
            {
                throw new NotFoundException(nameof(Option), request.Id);
            }

            var model = OptionDetailModel.Create(option);
            return new ResponseModel<OptionDetailModel>(model);
        }
    }
}