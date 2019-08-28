using System;

namespace Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser
{
    /// <summary>
    /// /api/TraineeUser/Post service return that model.
    /// </summary>
    public struct CreateTraineeUserModel
    {
        public int Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Name { get; }
        public string Surname { get; }
        public string NationalIdentityNumber { get; }
        public string PhoneNumber { get; }
        public int Gender { get; }

        public CreateTraineeUserModel(int id, DateTimeOffset createdAt, string name, string surname, string nationalIdentityNumber, string phoneNumber, int gender)
        {
            Id = id;
            CreatedAt = createdAt;
            Name = name;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

    }
}
