using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Streamer;Trusted_Connection=true;MultipleActiveResultSets=true;Integrated Security= true;TrustServerCertificate=True;")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>().HasMany(m => m.Videos).WithOne(m => m.Streamer).HasForeignKey(m => m.StreamerId).OnDelete(DeleteBehavior.Restrict); //Relacion 1 a muchos

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }

    }
}
