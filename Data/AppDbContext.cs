using Microsoft.EntityFrameworkCore;
using APIdemo.Models;

namespace APIdemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Simple configurations
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<Lesson>().HasKey(l => l.Id);
            modelBuilder.Entity<Enrollment>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
        }
    }
}
