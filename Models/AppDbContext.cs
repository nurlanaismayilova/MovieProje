// Models/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace MovieProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; } // and other entities
    }
}
