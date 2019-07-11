using System;

namespace Honoplay.Application.Trainers.Commands.CreateTrainer
{
    public struct CreateTrainerModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public int DepartmentId { get; }
        public int ProfessionId { get; }

        public CreateTrainerModel(int id, DateTimeOffset createdAt, string name, string surname, string email, string phoneNumber, int departmentId, int professionId)
        {
            Id = id;
            CreatedAt = createdAt;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentId = departmentId;
            ProfessionId = professionId;
        }
    }
}
