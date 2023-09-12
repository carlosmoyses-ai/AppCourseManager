using CourseManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Api.Data;
public class AppCourseManagerDbContext : DbContext
{
    public AppCourseManagerDbContext(DbContextOptions<AppCourseManagerDbContext> options) : base(options)
    {
    }

    public DbSet<StudentModel> Students { get; set; }
    public DbSet<CourseModel> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=.\\DataBase\\CourseManager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseModel>()
            .ToTable("Courses")
            .HasKey(c => c.CourseId);

        modelBuilder.Entity<CourseModel>()
            .Property(c => c.CourseId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<StudentModel>()
            .ToTable("Students")
            .HasKey(a => a.StudentId);

        modelBuilder.Entity<StudentModel>()
            .Property(a => a.StudentId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<CourseModel>()
            .HasMany(c => c.Students)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseStudent",
                s => s
                    .HasOne<StudentModel>()
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .HasConstraintName("FK_CourseStudent_Students_StudentId")
                    .OnDelete(DeleteBehavior.Cascade),
                c => c
                    .HasOne<CourseModel>()
                    .WithMany()
                    .HasForeignKey("CourseId")
                    .HasConstraintName("FK_CourseStudent_Courses_CourseId")
                    .OnDelete(DeleteBehavior.Cascade),
                sC =>
                {
                    sC.HasKey("CourseId", "StudentId");
                    sC.ToTable("CourseStudent");
                }
            );

        modelBuilder.Entity<StudentModel>()
            .HasMany(a => a.Courses)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseStudent",
                c => c
                    .HasOne<CourseModel>()
                    .WithMany()
                    .HasForeignKey("CourseId")
                    .HasConstraintName("FK_CourseStudent_Courses_CourseId")
                    .OnDelete(DeleteBehavior.Cascade),
                s => s
                    .HasOne<StudentModel>()
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .HasConstraintName("FK_CourseStudent_Students_StudentId")
                    .OnDelete(DeleteBehavior.Cascade),
                cS =>
                {
                    cS.HasKey("CourseId", "StudentId");
                    cS.ToTable("CourseStudent");
                }
            );
    }
}