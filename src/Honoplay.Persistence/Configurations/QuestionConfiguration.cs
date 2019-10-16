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

            //QuestionType
            builder.HasOne(q => q.QuestionType)
                .WithMany(qt => qt.Questions)
                .HasForeignKey(q => q.QuestionTypeId);
            
            //ContentFile
            builder.HasOne(q => q.ContentFile)
                .WithMany(cf => cf.Questions)
                .HasForeignKey(q => q.VisualId);

        }
    }
}
