using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeCommand : IRequest<ResponseModel<CreateTraineeModel>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }
        public int AdminUserId { get; set; }
    }
}
