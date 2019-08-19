using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheService;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common.Extensions;

namespace Honoplay.Application.Professions.Commands.CreateProfession
{
    public class CreateProfessionCommandHandler : IRequestHandler<CreateProfessionCommand, ResponseModel<CreateProfessionModel>>
    {
        private readonly HonoplayDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateProfessionCommandHandler(HonoplayDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<ResponseModel<CreateProfessionModel>> Handle(CreateProfessionCommand request,
            CancellationToken cancellationToken)
        {
            var newProfessions = new List<Profession>();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                var redisKey = $"ProfessionsByTenantId{request.TenantId}";
                try
                {
                    var professionsByTenantId = await _context.Professions
                        .Where(x => x.TenantId == request.TenantId)
                        .ToListAsync(cancellationToken);

                    foreach (var requestProfession in request.Professions)
                    {
                        var profession = new Profession
                        {
                            CreatedBy = request.AdminUserId,
                            Name = requestProfession,
                            TenantId = request.TenantId

                        };
                        newProfessions.Add(profession);
                    }

                    await _context.Professions.AddRangeAsync(newProfessions, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await _cacheService.RedisCacheUpdateAsync(redisKey,
                        _ => professionsByTenantId,
                        cancellationToken);

                    transaction.Commit();
                }
                catch (DbUpdateException ex) when ((ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601)) ||
                                                   (ex.InnerException is SqliteException sqliteException && sqliteException.SqliteErrorCode == 19))
                {
                    transaction.Rollback();

                    throw new ObjectAlreadyExistsException(nameof(Profession), ExceptionMessageExtensions.GetExceptionMessage(ex));
                }
                catch (NotFoundException)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new TransactionException();
                }
            }

            var professions = new CreateProfessionModel(newProfessions);
            return new ResponseModel<CreateProfessionModel>(professions);
        }
    }
}

