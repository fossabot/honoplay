using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Honoplay.Domain.Entities
{
    public class AdminUser
    {
        public AdminUser()
        {
            //Default values for non nullable refs.
            Email = UserName = Name = Surname = "";

            RowVersion = Password = PasswordSalt = new byte[0];

            TenantAdminUsers = new HashSet<TenantAdminUser>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int TimeZone { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }

        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset LastPasswordChangeDateTime { get; set; }
        public int NumberOfInvalidPasswordAttemps { get; set; }

        public byte[] RowVersion { get; set; }
        [JsonIgnore]

        public virtual ICollection<TenantAdminUser> TenantAdminUsers { get; set; }
    }
}