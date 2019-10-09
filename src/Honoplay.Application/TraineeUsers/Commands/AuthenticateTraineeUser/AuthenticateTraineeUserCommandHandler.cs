using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser
{
    public class AuthenticateTraineeUserCommandHandler : IRequestHandler<AuthenticateTraineeUserCommand, TraineeUserAuthenticateModel>
    {
        private readonly HonoplayDbContext _context;

        public AuthenticateTraineeUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<TraineeUserAuthenticateModel> Handle(AuthenticateTraineeUserCommand request, CancellationToken cancellationToken)
        {
            var traineeUser = await _context.TraineeUsers
                                    .SingleOrDefaultAsync(u =>
                                            u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase),
                                    cancellationToken);

            if (traineeUser is null)
            {
                throw new NotFoundException(nameof(request.Email), request.Email);
            }

            var salt = traineeUser.PasswordSalt;
            var passwordHash = request.Password?.GetSHA512(salt);
            var today = DateTimeOffset.Now;
            var isPasswordExpired = today.Subtract(traineeUser.LastPasswordChangeDateTime).Days > 90;

            if (!passwordHash.SequenceEqual(traineeUser.Password))
            {
                traineeUser.NumberOfInvalidPasswordAttemps = Math.Min(traineeUser.NumberOfInvalidPasswordAttemps + 1, int.MaxValue);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //TODO https://docs.microsoft.com/en-us/ef/core/saving/concurrency
                }
                catch (Exception)
                {
                    //TODO
                    throw;
                }
                //TODO:  Auth exception
                throw new Exception();
            }

            Guid tenantId;
            int departmentId;
            try
            {
                var currentDepartment = _context.TraineeUsers
                    .Include(x => x.Department.Tenant)
                    .First(x => x.Id == traineeUser.Id && x.Department.Tenant.HostName == request.HostName).Department;

                tenantId = currentDepartment.TenantId;
                departmentId = currentDepartment.Id;
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }


            return new TraineeUserAuthenticateModel(id: traineeUser.Id,
                                           email: traineeUser.Email,
                                           name: traineeUser.Name,
                                           isPasswordExpired: isPasswordExpired,
                                           departmentId: departmentId,
                                           tenantId: tenantId,
                                           hostName: request.HostName,
                                           avatarId: traineeUser.AvatarId);
        }
    }
}