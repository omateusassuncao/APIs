using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Data;
using ProfessoresApi.Models;

namespace ProfessoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{
    //private static List<Professor> professores = new List<Professor>();
    //private static int Id = 0;

    private ProfessorContext _context;

    public ProfessorController(ProfessorContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaProfessor([FromBody] Professor professor)
    {
        //professor.Id = Id++;
        //professores.Add(professor);

        _context.Professores.Add(professor);
        _context.SaveChanges(); 
        return CreatedAtAction(nameof(RecuperaProfessorPorId),new {id = professor.Id }, professor);
    }

    [HttpGet]
    public IEnumerable<Professor> RecuperaProfessor(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        //return professores.Skip(skip).Take(take);
        return _context.Professores.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaProfessorPorId(int id)
    {
        //var professor = professores.FirstOrDefault(professor => professor.Id == id);

        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return NotFound();
        return Ok(professor);
    }

}
