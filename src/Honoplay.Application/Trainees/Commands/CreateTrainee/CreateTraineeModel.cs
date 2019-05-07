namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public struct CreateTraineeModel
    {
        public string Name { get; }
        public string Surname { get; }
        public string NationalIdentityNumber { get; }
        public string PhoneNumber { get; }
        public int Gender { get; }

        public CreateTraineeModel(string name, string surname, string nationalIdentityNumber, string phoneNumber, int gender)
        {
            Name = name;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

    }
}
