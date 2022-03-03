using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.Domain;
using KafkaMessagingQueue.Persistence.Configuration;

namespace KafkaMessagingQueue.Persistence
{
    public class DatabaseContext : DbContext
    {
        private readonly ContextConfiguration contextConfiguration;
        
        public DatabaseContext(ContextConfiguration contextConfiguration)
        {
            this.contextConfiguration = contextConfiguration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (contextConfiguration.Type == "InMemory")
                optionsBuilder.UseInMemoryDatabase(contextConfiguration.ConnectionString);
            else
                optionsBuilder.UseNpgsql(contextConfiguration.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuideConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
