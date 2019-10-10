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
        public int? AvatarId { get; set; }

        public UpdateTraineeUserModel(int id, string name, string email, string surname, string nationalIdentityNumber, string phoneNumber, int gender, int? avatarId, DateTimeOffset updatedAt)
        {
            Id = id;
            UpdatedAt = updatedAt;
            Name = name;
            Email = email;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            PhoneNumber = phoneNumber;
            Gender = gender;
            AvatarId = avatarId;
        }

    }
}
