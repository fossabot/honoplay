using Honoplay.Application._Infrastructure;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser
{
    public class AuthenticateTrainerUserCommandHandler : IRequestHandler<AuthenticateTrainerUserCommand, TrainerUserAuthenticateModel>
    {
        private readonly HonoplayDbContext _context;

        public AuthenticateTrainerUserCommandHandler(HonoplayDbContext context)
        {
            _context = context;
        }

        public async Task<TrainerUserAuthenticateModel> Handle(AuthenticateTrainerUserCommand request, CancellationToken cancellationToken)
        {
            var jsValidators = JsValidators.GetAllJsValidations();

            var trainerUser = await _context.TrainerUsers
                                    .SingleOrDefaultAsync(u =>
                                            u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase),
                                    cancellationToken);

            if (trainerUser is null)
            {
                throw new NotFoundException(nameof(request.Email), request.Email);
            }

            var salt = trainerUser.PasswordSalt;
            var passwordHash = request.Password?.GetSHA512(salt);
            var today = DateTimeOffset.Now;
            var isPasswordExpired = today.Subtract(trainerUser.LastPasswordChangeDateTime).Days > 90;

            if (!passwordHash.SequenceEqual(trainerUser.Password))
            {
                trainerUser.NumberOfInvalidPasswordAttemps = Math.Min(trainerUser.NumberOfInvalidPasswordAttemps + 1, int.MaxValue);
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

            int departmentId;
            try
            {
                departmentId = _context.TrainerUsers
                    .Include(x => x.Department.Tenant)
                    .First(x => x.Id == trainerUser.Id && x.Department.Tenant.HostName == request.HostName).DepartmentId;
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }


            return new TrainerUserAuthenticateModel(id: trainerUser.Id,
                                           email: trainerUser.Email,
                                           name: trainerUser.Name,
                                           isPasswordExpired: isPasswordExpired,
                                           departmentId: departmentId,
                                           hostName: request.HostName,
                                           jsValidators: jsValidators);
        }
    }
}