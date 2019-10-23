using System;

namespace Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser
{
    public struct TraineeUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string Surname { get; }
        public int DepartmentId { get; }
        public Guid TenantId { get; }
        public int? AvatarId { get; set; }
        public string HostName { get; }
        public bool IsPasswordExpired { get; }

        public TraineeUserAuthenticateModel(int id, string email, string name, string surname, int departmentId, Guid tenantId, int? avatarId, string hostName, bool isPasswordExpired)
        {
            Id = id;
            Email = email;
            Name = name;
            Surname = surname;
            DepartmentId = departmentId;
            TenantId = tenantId;
            AvatarId = avatarId;
            HostName = hostName;
            IsPasswordExpired = isPasswordExpired;
        }
    }
}
