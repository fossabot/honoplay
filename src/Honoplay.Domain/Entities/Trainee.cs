using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Honoplay.Domain.Entities
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public int WorkingStatusId { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public WorkingStatus WorkingStatus { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
