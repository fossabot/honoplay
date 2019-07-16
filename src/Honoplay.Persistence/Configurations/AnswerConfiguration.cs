using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Text
            builder.Property(x => x.Text)
                .HasMaxLength(500);

            //Answer
            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(q => q.QuestionId);
        }
    }
}
