using System;

namespace Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser
{
    public struct UpdateTraineeUserModel
    {
        public int Id { get; }
        public DateTimeOffset UpdatedAt { get; }
        public string Name { get; }
        public string Email { get; }
        public string Surname { get; }
        public string NationalIdentityNumber { get; }
        public string PhoneNumber { get; }
        public int Gender { get; }


        public UpdateTraineeUserModel(int id, string name, string email, string surname, string nationalIdentityNumber, string phoneNumber, int gender, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            PhoneNumber = phoneNumber;
            Gender = gender;
            UpdatedAt = updatedAt;
        }

    }
}
