using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeturAssessment.ReportApi.Application.Domain;

namespace SeturAssessment.ReportApi.Application.Persistence.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Data)
                .HasMaxLength(int.MaxValue);

            builder.Property(x => x.CreateBy).IsRequired().HasMaxLength(200);
        }
    }
}
