using Microsoft.EntityFrameworkCore;
using SeturAssessment.Domain;
using SeturAssessment.Persistence.Configuration;

namespace SeturAssessment.Persistence
{
    public class SeturContext : DbContext
    {
        public SeturContext(DbContextOptions<SeturContext> options) : base(options)
        {
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
