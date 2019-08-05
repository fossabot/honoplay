using EFCore.BulkExtensions;
using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.Sessions.Commands.CreateSession
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, ResponseModel<List<CreateSessionModel>>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateSessionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        public async Task<ResponseModel<List<CreateSessionModel>>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var redisKey = $"SessionsByTenantId{request.TenantId}";
            var newSessions = new List<Session>();
            var createdSessions = new List<CreateSessionModel>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var createSessionModel in request.CreateSessionModels)
                    {
                        var newSession = new Session
                        {
                            CreatedBy = request.CreatedBy,
                            GameId = createSessionModel.GameId,
                            Name = createSessionModel.Name,
                            ClassroomId = createSessionModel.ClassroomId
                        };
                        newSessions.Add(newSession);
                    }

                    if (newSessions.Count > 20)
                    {
                        _context.BulkInsert(newSessions);
                    }
                    else
                    {
                        await _context.Sessions.AddRangeAsync(newSessions, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    transaction.Commit();
                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => _context.Sessions
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken),
                        cancellationToken);

                    newSessions.ForEach(x =>
                        createdSessions.Add(new CreateSessionModel(x.Id, x.GameId, x.ClassroomId, x.Name, x.CreatedBy, x.CreatedAt)));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            return new ResponseModel<List<CreateSessionModel>>(createdSessions);
        }
    }
}
