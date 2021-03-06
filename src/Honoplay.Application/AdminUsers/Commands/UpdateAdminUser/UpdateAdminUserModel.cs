﻿using System;

namespace Honoplay.Application.AdminUsers.Commands.UpdateAdminUser
{
    public struct UpdateAdminUserModel
    {
        public int? Id { get; }
        public string Email { get; }
        public string Username { get; }
        public string Name { get; }
        public string Surname { get; }
        public string PhoneNumber { get; }
        public int TimeZone { get; }

        public DateTimeOffset? CreatedDateTime { get; }

        public UpdateAdminUserModel(int id, string email, string username, string name, string surname, string phoneNumber, int timeZone, DateTimeOffset createDateTime)
        {
            Id = id;
            Email = email;
            Username = username;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            TimeZone = timeZone;
            CreatedDateTime = createDateTime;
        }
    }
}