using CourseManager.Api.Data;
using CourseManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly AppCourseManagerDbContext _context;
    public StudentController(AppCourseManagerDbContext context) => _context = context;

    // GET: api/Students
    [HttpGet("GetStudents")]
    public async Task<ActionResult<StudentModel>> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        await _context.SaveChangesAsync();
        return (students.Count == 0) ? NoContent() : Ok(students);
    }

    // GET: api/Students/5
    [HttpGet("GetStudent/{id}")]
    public async Task<ActionResult<StudentModel>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        await _context.SaveChangesAsync();
        return (student == null) ? NotFound() : Ok(student);
    }

    // GET: api/Students/5/Courses
    [HttpGet("GetStudent/{id}/Courses")]
    public async Task<ActionResult<StudentModel>> GetStudentCourses(int id)
    {
        var student = await _context.Students
                .Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == id);
        await _context.SaveChangesAsync();
        return (student == null) ? NotFound() : Ok(student.Courses);
    }

    // PUT: api/Students/5
    [HttpPut("PutStudent/{id}")]
    public async Task<IActionResult> PutStudent(int id, StudentModel student)
    {
        var studentUpdate = await _context.Students.FindAsync(id);
        if (studentUpdate == null)
        {
            return NotFound();
        }
        studentUpdate.StudentName = student.StudentName;
        studentUpdate.Email = student.Email;
        studentUpdate.EnrollmentDate = student.EnrollmentDate;
        await _context.SaveChangesAsync();
        return Ok("Aluno atualizado com sucesso!");
    }

    // PUT: api/Students/5/AddCourse
    [HttpPut("PutStudent/{id}/AddCourse")]
    public async Task<IActionResult> PutStudenAddCourse(int id, int courseId)
    {
        var selectedStudent = await _context.Students
                .Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == id);
        var selectedCourse = await _context.Courses
                .Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == courseId);
        if (selectedStudent == null || selectedCourse == null)
        {
            return NotFound();
        }
        selectedStudent.Courses!.Add(selectedCourse);
        await _context.SaveChangesAsync();
        return Ok("Curso adicionado com sucesso!");
    }

    // POST: api/Students
    [HttpPost("PostStudent")]
    public async Task<ActionResult<StudentModel>> PostStudent(StudentModel student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
    }

    // DELETE: api/Students/5
    [HttpDelete("DeleteStudent/{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var studentDelete = await _context.Students.FindAsync(id);
        if (studentDelete == null)
            return NotFound();
        _context.Students.Remove(studentDelete);
        await _context.SaveChangesAsync();
        return Ok("Aluno removido com sucesso!");
    }

    // DELETE: api/Students/5/RemoveCourse
    [HttpDelete("DeleteStudent/{id}/RemoveCourse")]
    public async Task<IActionResult> DeleteStudentRemoveCourse(int id, int courseId)
    {
        var selectedStudent = await _context.Students
                .Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == id);
        var selectedCourse = await _context.Courses
                .Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == courseId);
        if (selectedStudent == null || selectedCourse == null)
        {
            return NotFound();
        }
        selectedStudent.Courses!.Remove(selectedCourse);
        await _context.SaveChangesAsync();
        return Ok("Curso removido com sucesso!");
    }
}