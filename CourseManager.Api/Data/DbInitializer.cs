using CourseManager.Api.Models;

namespace CourseManager.Api.Data;

public class DbInitializer
{
    public static void Initialize(AppCourseManagerDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Courses.Any())
        {
            var Course = new CourseModel[]
            {
                new CourseModel
                {
                    CourseName = "Course de C#",
                    Description= "Course de C# com .NET Core",
                    StartDate = DateTime.Parse("2021-01-01"),
                    EndDate = DateTime.Parse("2021-12-31")
                },
                new CourseModel
                {
                    CourseName = "Course de Java",
                    Description= "Course de Java com Spring Boot",
                    StartDate = DateTime.Parse("2021-01-01"),
                    EndDate = DateTime.Parse("2021-12-31")
                },
            };
            context.Courses.AddRange(Course);
        }

        if (!context.Students.Any())
        {
            var alunos = new StudentModel[]
            {
                new StudentModel
                {
                    StudentName = "Carlos",
                    Email = "carlos@gmail.com",
                    EnrollmentDate = DateTime.Parse("2021-01-01")
                },
            };
            context.Students.AddRange(alunos);
        }

        context.SaveChanges();
    }
}
