using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Honoplay.Application.Exceptions;
#nullable enable

namespace Honoplay.Application.Tokens.Commands
{
    public class GetAdminUserTokenCommandHandler : IRequestHandler<GetAdminUserTokenCommand>
    {
        private readonly HonoplayDbContext _context;

        public GetAdminUserTokenCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(GetAdminUserTokenCommand request, CancellationToken cancellationToken)
        {
            var adminUser = await _context.AdminUsers
                                    .Where(u => u.Username == request.Username
                                             && u.TenantId == request.TenantId)
                                    .ToListAsync();

            if(adminUser.Count < 1 || adminUser.Count > 1)
            {
                throw new NotFoundException(nameof(request.Username), request.Username);
            }



            return default;
        }
    }
}
