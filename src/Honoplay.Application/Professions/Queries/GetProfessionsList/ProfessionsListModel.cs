using Honoplay.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Honoplay.Application.Professions.Queries.GetProfessionsList
{
    public struct ProfessionsListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public static Expression<Func<Profession, ProfessionsListModel>> Projection
        {
            get
            {
                return profession => new ProfessionsListModel
                {
                    Id = profession.Id,
                    Name = profession.Name,
                    TenantId = profession.TenantId,
                    CreatedBy = profession.CreatedBy,
                    UpdatedBy = profession.UpdatedBy,
                    UpdatedAt = profession.UpdatedAt,
                    CreatedAt = profession.CreatedAt
                };
            }
        }
        public static ProfessionsListModel Create(Profession profession)
        {
            return Projection.Compile().Invoke(profession);
        }
    }
}
