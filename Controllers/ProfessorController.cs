using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Models;

namespace ProfessoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{

    private static List<Professor> professores = new List<Professor>();

    [HttpPost]
    public void AdicionaProfessor([FromBody] Professor professor)
    {
        professores.Add(professor);
        Console.WriteLine(professor.Nome + " / " + professor.Disciplina);
    }

}
