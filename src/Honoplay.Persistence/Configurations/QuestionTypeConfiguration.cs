using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
