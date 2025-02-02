using Microsoft.AspNetCore.Mvc;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, [FromBody] Student updatedStudent)
    {
        if (updatedStudent == null)
        {
            return BadRequest("Dados inválidos.");
        }

        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound("Aluno não encontrado.");
        }

        student.Name = updatedStudent.Name;
        student.Email = updatedStudent.Email;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound("Aluno não encontrado.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound("Aluno não encontrado.");
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();  
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
