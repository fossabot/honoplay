using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public struct UpdateTraineeModel
    {
        public int Id { get; set; }
        public string Name { get; }
        public string Surname { get; }
        public string NationalIdentityNumber { get; }
        public string PhoneNumber { get; }
        public int Gender { get; }

        public UpdateTraineeModel(int id,string name, string surname, string nationalIdentityNumber, string phoneNumber, int gender)
        {
            Id = id;
            Name = name;
            Surname = surname;
            NationalIdentityNumber = nationalIdentityNumber;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

    }
}
