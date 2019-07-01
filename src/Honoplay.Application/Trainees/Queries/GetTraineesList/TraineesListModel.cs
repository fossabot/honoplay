using System;
using System.Linq.Expressions;
using Honoplay.Domain.Entities;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public struct TraineesListModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }

        public static Expression<Func<Trainee, TraineesListModel>> Projection => trainee => new TraineesListModel
        {
            Name = trainee.Name,
            Surname = trainee.Surname,
            NationalIdentityNumber = trainee.NationalIdentityNumber,
            PhoneNumber = trainee.PhoneNumber,
            Gender = trainee.Gender
        };

        public static TraineesListModel Create(Trainee trainee)
        {
            return Projection.Compile().Invoke(trainee);
        }
    }
}
