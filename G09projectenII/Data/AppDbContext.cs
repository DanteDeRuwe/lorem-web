using Microsoft.EntityFrameworkCore;

namespace G09projectenII.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionCalendar> SessionCalendars { get; set; }

        // finds all classes that implement IEntityTypeConfiguration and applies their configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
