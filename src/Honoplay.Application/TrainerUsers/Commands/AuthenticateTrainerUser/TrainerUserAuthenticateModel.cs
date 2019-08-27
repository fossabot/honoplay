﻿using System;

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
        public string JsValidators { get; }

        public TrainerUserAuthenticateModel(int id, string email, string name, bool isPasswordExpired, int departmentId, Guid tenantId, string hostName, string jsValidators)
        {
            Id = id;
            Email = email;
            Name = name;
            IsPasswordExpired = isPasswordExpired;
            DepartmentId = departmentId;
            TenantId = tenantId;
            HostName = hostName;
            JsValidators = jsValidators;
        }
    }
}
