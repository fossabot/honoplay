using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Text
            builder.Property(x => x.Text)
                .HasMaxLength(500);

            //RELATIONS

            //Tenant
            builder.HasOne(q => q.Tenant)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TenantId);


        }
    }
}
