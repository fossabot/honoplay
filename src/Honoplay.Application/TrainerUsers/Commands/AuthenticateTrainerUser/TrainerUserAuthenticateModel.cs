using System;

namespace Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser
{
    public struct TrainerUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public int DepartmentId { get; }
        public Guid TenantId { get; }
        public string HostName { get; }
        public bool IsPasswordExpired { get; }

        public TrainerUserAuthenticateModel(int id, string email, string name, int departmentId, Guid tenantId, string hostName, bool isPasswordExpired)
        {
            Id = id;
            Email = email;
            Name = name;
            DepartmentId = departmentId;
            TenantId = tenantId;
            HostName = hostName;
            IsPasswordExpired = isPasswordExpired;
        }
    }
}
