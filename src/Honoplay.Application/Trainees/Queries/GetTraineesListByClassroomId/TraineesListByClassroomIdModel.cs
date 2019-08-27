using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Trainees.Queries.GetTraineesListByClassroomId
{
    public struct GetTraineesListByClassroomIdModel
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }


        public static Expression<Func<Trainee, GetTraineesListByClassroomIdModel>> Projection
        {
            get
            {
                return trainee => new GetTraineesListByClassroomIdModel
                {
                    Id = trainee.Id,
                    CreatedAt = trainee.CreatedAt,
                    CreatedBy = trainee.CreatedBy,
                    UpdatedBy = trainee.UpdatedBy,
                    UpdatedAt = trainee.UpdatedAt,
                    Gender = trainee.Gender,
                    PhoneNumber = trainee.PhoneNumber,
                    NationalIdentityNumber = trainee.NationalIdentityNumber,
                    Surname = trainee.Surname,
                    Name = trainee.Name,
                    WorkingStatusId = trainee.WorkingStatusId,
                    DepartmentId = trainee.DepartmentId
                };
            }
        }
        public static GetTraineesListByClassroomIdModel Create(Trainee classroom)
        {
            return Projection.Compile().Invoke(classroom);
        }
    }
}
