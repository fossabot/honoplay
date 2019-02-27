using Microsoft.AspNetCore.Identity;
using System;

#nullable enable
namespace Honoplay.Domain.Entities
{
    public sealed class AdminUser : IdentityUser<int>
    {

        public AdminUser()
        {
            //Default values for non nullable refs.
            Email = UserName = Name = Surname = "";

            RowVersion = Password = PasswordSalt = new byte[0];
        }

        public override int Id { get; set; }
        public override string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string? PhoneNumber { get; set; }
        public override string Email { get; set; }
        public int TimeZone { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }

        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset LastPasswordChangeDateTime { get; set; }
        public int NumberOfInvalidPasswordAttemps { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
