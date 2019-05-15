using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Tenants.Queries.GetTraineeDetail
{
    public struct TraineeDetailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }

        private static Expression<Func<Trainee, TraineeDetailModel>> Projection
        {
            get
            {
                return trainee => new TraineeDetailModel
                {
                    Gender = trainee.Gender,
                    PhoneNumber = trainee.PhoneNumber,
                    NationalIdentityNumber = trainee.NationalIdentityNumber,
                    Surname = trainee.Surname,
                    Name = trainee.Name
                };
            }
        }

        public static TraineeDetailModel Create(Trainee trainee)
        {
            return Projection.Compile().Invoke(trainee);
        }
    }
}