using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Models;

namespace ProfessoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{

    private static List<Professor> professores = new List<Professor>();
    private static int Id = 0;

    [HttpPost]
    public IActionResult AdicionaProfessor([FromBody] Professor professor)
    {
        professor.Id = Id++;
        professores.Add(professor);
        return CreatedAtAction(nameof(RecuperaProfessorPorId),new {id = professor.Id }, professor);
    }

    [HttpGet]
    public IEnumerable<Professor> RecuperaProfessor(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return professores.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaProfessorPorId(int id)
    {
        var professor = professores.FirstOrDefault(professor => professor.Id == id);
        if (professor == null) return NotFound();
        return Ok();
    }

}
