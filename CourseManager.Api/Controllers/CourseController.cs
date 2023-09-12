using CourseManager.Api.Data;
using CourseManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly AppCourseManagerDbContext _context;
    public CourseController(AppCourseManagerDbContext context) => _context = context;

    // GET: api/Courses
    [HttpGet("GetCourses")]
    public async Task<ActionResult<CourseModel>> GetCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        await _context.SaveChangesAsync();
        return (courses.Count == 0) ? NoContent() : Ok(courses);
    }

    // GET: api/Courses/5
    [HttpGet("GetCourse/{id}")]
    public async Task<ActionResult<CourseModel>> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        await _context.SaveChangesAsync();
        return (course == null) ? NotFound() : Ok(course);
    }

    // GET: api/Courses/5/Students
    [HttpGet("GetCourse/{id}/Students")]
    public async Task<ActionResult<CourseModel>> GetCourseStudents(int id)
    {
        var course = await _context.Courses
                .Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == id);
        await _context.SaveChangesAsync();
        return (course == null) ? NotFound() : Ok(course.Students);
    }

    // PUT: api/Courses/5
    [HttpPut("PutCourse/{id}")]
    public async Task<IActionResult> PutCourse(int id, CourseModel course)
    {
        var courseUpdate = await _context.Courses.FindAsync(id);
        if (courseUpdate == null)
        {
            return NotFound();
        }
        courseUpdate.CourseName = course.CourseName;
        courseUpdate.Description = course.Description;
        courseUpdate.StartDate = course.StartDate;
        courseUpdate.EndDate = course.EndDate;
        await _context.SaveChangesAsync();
        return Ok("Curso atualizado com sucesso!");
    }

    // PUT: api/Courses/5/AddStudent
    [HttpPut("PutCourse/{id}/AddStudent")]
    public async Task<IActionResult> PutCourseAddCourse(int id, int studentId)
    {
        var selectedStudent = await _context.Students
                .Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == studentId);
        var selectedCourse = await _context.Courses
                .Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == id);
        if (selectedStudent == null || selectedCourse == null)
        {
            return NotFound();
        }
        selectedCourse.Students!.Add(selectedStudent);
        await _context.SaveChangesAsync();
        return Ok("Aluno adicionado com sucesso!");
    }

    // POST: api/Courses
    [HttpPost("PostCourse")]
    public async Task<ActionResult<CourseModel>> PostCourse(CourseModel course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
    }

    // DELETE: api/Courses/5
    [HttpDelete("DeleteCourse/{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var courseDelete = await _context.Courses.FindAsync(id);
        if (courseDelete == null) return NotFound();
        _context.Courses.Remove(courseDelete);
        await _context.SaveChangesAsync();
        return Ok("Curso removido com sucesso!");
    }

    // DELETE: api/Courses/5/RemoveStudent
    [HttpDelete("GetCourse/{id}/RemoveStudent")]
    public async Task<IActionResult> DeleteCourseRemoveStudent(int id, int studentId)
    {
        var selectedStudent = await _context.Students
                .Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == studentId);
        var selectedCourse = await _context.Courses
                .Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == id);
        if (selectedStudent == null || selectedCourse == null)
        {
            return NotFound();
        }
        selectedCourse.Students!.Remove(selectedStudent);
        await _context.SaveChangesAsync();
        return Ok("Aluno removido com sucesso!");
    }
}