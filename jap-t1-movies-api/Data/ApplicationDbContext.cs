using JAP_Task_1_MoviesApi.Extensions;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JAP_Task_1_MoviesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<MostRatedMoviesReport> MostRatedMoviesReports { get; set; }
        public DbSet<MoviesWithMostScreeningsReport> MoviesWithMostScreeningsReports { get; set; }
        public DbSet<MoviesWithMostSoldTicketsReport> MoviesWithMostSoldTicketsReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.Entity<MostRatedMoviesReport>().HasNoKey();

            modelBuilder.Entity<MoviesWithMostScreeningsReport>().HasNoKey();

            modelBuilder.Entity<MoviesWithMostSoldTicketsReport>().HasNoKey();
        }
    }
}
