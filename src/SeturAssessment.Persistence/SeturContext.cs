using Microsoft.EntityFrameworkCore;
using SeturAssessment.Domain;
using SeturAssessment.Persistence.Configuration;

namespace SeturAssessment.Persistence
{
    public class SeturContext : DbContext
    {
        private readonly ContextConfiguration contextConfiguration;
        
        public SeturContext(ContextConfiguration contextConfiguration)
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

    public class ContextConfiguration
    {
        public string ConnectionString { get; set; }
        public string Type { get; set; }
    }
}
