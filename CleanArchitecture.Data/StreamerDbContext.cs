using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MagicVilla;Trusted_Connection=true;MultipleActiveResultSets=true;Integrated Security= true;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }

    }
}
