using Microsoft.AspNetCore.Mvc; // Necessário para ControllerBase, ApiController, etc.
using Backend.Data; // Para o ApplicationDbContext
using Backend.Models; // Para o modelo Student
using Microsoft.EntityFrameworkCore; // Para o Entity Framework


[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        if (student == null)
        {
            return BadRequest("Dados inválidos.");
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudents), new { id = student.Id }, student);
    }
}
