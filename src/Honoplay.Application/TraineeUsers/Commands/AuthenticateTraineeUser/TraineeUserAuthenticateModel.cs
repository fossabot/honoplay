using System;

namespace Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser
{
    public struct TraineeUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public int DepartmentId { get; }
        public Guid TenantId { get; }
        public int? AvatarId { get; set; }
        public string HostName { get; }
        public bool IsPasswordExpired { get; }

        public TraineeUserAuthenticateModel(int id, string email, string name, bool isPasswordExpired, int departmentId, Guid tenantId, string hostName, int? avatarId)
        {
            Id = id;
            Email = email;
            Name = name;
            IsPasswordExpired = isPasswordExpired;
            DepartmentId = departmentId;
            TenantId = tenantId;
            HostName = hostName;
            AvatarId = avatarId;
        }
    }
}
