using Microsoft.EntityFrameworkCore;
using MovieDBMS.Models;

namespace MovieDBMS
{
    public class MovieDBContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MovieDBMS;Integrated Security=True;TrustServerCertificate=True;");
            //.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rating>().HasKey(r => new { r.UserID, r.MovieID });
        }
    }
}
