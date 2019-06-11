namespace Honoplay.Application.Trainers.Commands.CreateTrainer
{
    public struct CreateTrainerModel
    {
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public int DepartmentId { get; }
        public int ProfessionId { get; }

        public CreateTrainerModel(string name, string surname, string email, string phoneNumber, int departmentId, int professionId)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            DepartmentId = departmentId;
            ProfessionId = professionId;
        }
    }
}
