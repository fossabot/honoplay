using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;

namespace Honoplay.Domain.Entities
{
    public class Trainer : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ProfessionId { get; set; }

        [JsonIgnore]
        public Profession Profession { get; set; }
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
