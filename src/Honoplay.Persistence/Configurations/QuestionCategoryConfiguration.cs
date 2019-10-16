using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategory>
    {
        public void Configure(EntityTypeBuilder<QuestionCategory> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Text
            builder.Property(x => x.Name)
                .HasMaxLength(150);

            //RELATIONS

            //Tenant
            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.QuestionCategories)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
