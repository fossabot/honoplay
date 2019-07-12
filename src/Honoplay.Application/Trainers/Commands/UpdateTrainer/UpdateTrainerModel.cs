﻿using System;

namespace Honoplay.Application.Trainers.Commands.UpdateTrainer
{
    public struct UpdateTrainerModel
    {
        public int Id { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public int DepartmentId { get; private set; }
        public int ProfessionId { get; private set; }

        public UpdateTrainerModel(int id, DateTimeOffset updatedAt, string name, string surname, string email, string phoneNumber, int departmentId, int professionId)
        {
            Id = id;
            UpdatedAt = updatedAt;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentId = departmentId;
            ProfessionId = professionId;
        }
    }
}
