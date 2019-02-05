using System;

#nullable enable
namespace Honoplay.Domain.Entities
{
    public class AdminUser
    {

        public AdminUser()
        {
            //Default values for non nullable refs.
            Username = Name
                     = Surname
                     = "";

            RowVersion = Password
                       = PasswordSalt
                       = new byte[0];

            Tenant = new Tenant();
        }
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? PhoneNumber { get; set; }
        public string? EMailAddress { get; set; }
        public int TimeZone { get; set; }


        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset UpdatedDateTime { get; set; }

        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTimeOffset LastPasswordChangeDateTime { get; set; }
        public int NumberOfInvalidPasswordAttemps { get; set; }

        public byte[] RowVersion { get; set; }

        public Tenant Tenant { get; set; }
    }
}
