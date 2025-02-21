using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Mission6Movie.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Categories)  // Referring to the Categories property in Movie
                .WithMany(c => c.Movies)    // Referring to the Movies collection in Categories
                .HasForeignKey(m => m.CategoryId);  // Foreign key from Movie to Categories
            
            // Configure Categories entity's primary key
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.CategoryId);  // Set CategoryId as the primary key for Categories
            
        }
    }
}