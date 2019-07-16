using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainees.Queries.GetTraineesList
{
    public struct TraineesListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Trainee, TraineesListModel>> Projection => trainee => new TraineesListModel
        {
            Id = trainee.Id,
            Name = trainee.Name,
            Surname = trainee.Surname,
            NationalIdentityNumber = trainee.NationalIdentityNumber,
            PhoneNumber = trainee.PhoneNumber,
            Gender = trainee.Gender,
            UpdatedBy = trainee.UpdatedBy,
            UpdatedAt = trainee.UpdatedAt,
            CreatedAt = trainee.CreatedAt,
            CreatedBy = trainee.CreatedBy
        };

        public static TraineesListModel Create(Trainee trainee)
        {
            return Projection.Compile().Invoke(trainee);
        }
    }
}
