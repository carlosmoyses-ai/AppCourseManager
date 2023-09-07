using CourseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Api.Data;
public class AppCourseManagerDbContext : DbContext
{
    public AppCourseManagerDbContext(DbContextOptions<AppCourseManagerDbContext> options) : base(options)
    {
    }

    public DbSet<CourseModel> Courses { get; set; } = default!;
    public DbSet<StudentModel> Students { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=.\\DataBase\\coursemanager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseModel>()
            .HasMany(c => c.Students)
            .WithMany(s => s.Courses);
        modelBuilder.Entity<StudentModel>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students);
    }
}