namespace CourseManager.Api.Models;

public class CourseModel
{
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<StudentModel>? Students { get; internal set; }

    
}