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
    public void AdicionaProfessor([FromBody] Professor professor)
    {
        professor.Id = Id++;
        professores.Add(professor);
        Console.WriteLine(professor.Nome + " / " + professor.Disciplina);
    }

    [HttpGet]
    public IEnumerable<Professor> RecuperaFilmes(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return professores.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Professor? RecuperaProfessorPorId(int id)
    {
        return professores.FirstOrDefault(professor => professor.Id == id);
    }

}
