using Microsoft.EntityFrameworkCore;
using SeturAssessment.ReportApi.Application.Domain;
using SeturAssessment.ReportApi.Application.Persistence.Configuration;

namespace SeturAssessment.ReportApi.Application.Persistence
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
