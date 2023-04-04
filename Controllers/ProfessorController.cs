using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Data;
using ProfessoresApi.Data.Dtos;
using ProfessoresApi.Models;

namespace ProfessoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{
    //private static List<Professor> professores = new List<Professor>();
    //private static int Id = 0;

    private ProfessorContext _context;
    private IMapper _mapper;

    public ProfessorController(ProfessorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaProfessor([FromBody] CreateProfessorDto professorDto)
    {
        //professor.Id = Id++;
        //professores.Add(professor);

        Professor professor = _mapper.Map<Professor>(professorDto);
        _context.Professores.Add(professor);
        _context.SaveChanges(); 
        return CreatedAtAction(nameof(RecuperaProfessorPorId),new {id = professor.Id }, professor);
    }

    [HttpGet]
    public IEnumerable<ReadProfessorDto> RecuperaProfessor(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        //return professores.Skip(skip).Take(take);
        return _mapper.Map<List<ReadProfessorDto>>(_context.Professores.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaProfessorPorId(int id)
    {
        //var professor = professores.FirstOrDefault(professor => professor.Id == id);

        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return NotFound();
        var professorDto = _mapper.Map<ReadProfessorDto>(professor); 
        return Ok(professor);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaProfessor(int id, [FromBody] UpdateProfessorDto professorDto)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if(professor == null) return NotFound();

        _mapper.Map(professorDto, professor);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaProfessorPatch(int id,JsonPatchDocument<UpdateProfessorDto> patch)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return NotFound();

        var professorParaAtualizar = _mapper.Map<UpdateProfessorDto>(professor);

        patch.ApplyTo(professorParaAtualizar, ModelState);

        if(!TryValidateModel(professorParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(professorParaAtualizar, professor);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaProfessor(int id)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return NotFound();

        _context.Remove(professor);
        _context.SaveChanges();
        return NoContent();

    }

}
