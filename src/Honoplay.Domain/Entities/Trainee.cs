using Newtonsoft.Json;
using System.Collections.Generic;

namespace Honoplay.Domain.Entities
{
    public class TraineeUser : BaseEntity
    {
        public TraineeUser()
        {
            ClassroomTraineeUsers = new HashSet<ClassroomTraineeUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }


        public virtual ICollection<ClassroomTraineeUser> ClassroomTraineeUsers { get; set; }
        [JsonIgnore]
        public WorkingStatus WorkingStatus { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
