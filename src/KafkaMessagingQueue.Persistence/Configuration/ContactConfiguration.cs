using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KafkaMessagingQueue.Domain;

namespace KafkaMessagingQueue.Persistence.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts")
                .HasKey(x => x.Id);

            builder.Property(x => x.Value).IsRequired().HasMaxLength(200);
            
            builder.Property(x => x.CreateBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UpdateBy).HasMaxLength(200);
        }
    }
}
