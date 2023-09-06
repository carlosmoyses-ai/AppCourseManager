namespace CourseManager.Api.Models;

public class StudentModel
{
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? Email { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public ICollection<CourseModel>? Courses { get; internal set; }
}