using Honoplay.Domain.Entities;
using System.Collections.Generic;

namespace Honoplay.Application.Professions.Commands.CreateProfession
{
    public struct CreateProfessionModel
    {
        public ICollection<Profession> Professions { get; set; }

        public CreateProfessionModel(ICollection<Profession> professions)
        {
            Professions = professions;
        }
    }
}