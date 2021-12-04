using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeturAssessment.Domain;

namespace SeturAssessment.Persistence.Configuration
{
    public class GuideConfiguration : IEntityTypeConfiguration<Guide>
    {
        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.ToTable("Guides")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Company).HasMaxLength(300);

            builder.Property(x => x.CreateBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UpdateBy).HasMaxLength(200);

            builder.HasMany(x => x.Contacts)
                .WithOne(x => x.Guide)
                .HasForeignKey(x => x.GuideId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
