using Microsoft.EntityFrameworkCore;
using KafkaMessagingQueue.ReportApi.Application.Domain;
using KafkaMessagingQueue.ReportApi.Application.Persistence.Configuration;

namespace KafkaMessagingQueue.ReportApi.Application.Persistence
{
    public class ReportContext : DbContext
    {
        private readonly ContextConfiguration contextConfiguration;

        public ReportContext(ContextConfiguration contextConfiguration)
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
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
