using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Data;
using ProfessoresApi.Data.Dtos;
using ProfessoresApi.Models;
using ProfessoresApi.Services;

namespace ProfessoresApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfessorController : ControllerBase
{

    private ProfessorService _professorService;

    public ProfessorController(ProfessorService professorService)
    {
        _professorService = professorService;
    }

    /// <summary>
    /// Adicionar Professor ao bando de dados
    /// </summary>
    /// <param name="professorDto">Objeto com os parâmetros necessários para a criação de um professor</param>
    /// <returns>IActionResullt</returns>
    /// <response code='201'> Caso inserção com sucesso</response>response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaProfessor([FromBody] CreateProfessorDto professorDto)
    {
        ReadProfessorDto readDto =  _professorService.AdicionaProfessor(professorDto);
        return CreatedAtAction(nameof(RecuperaProfessorPorId), new { id = readDto.Id }, readDto);

    }

    /// <summary>
    /// Recupera lista de objetos Professor do bando de dados
    /// </summary>
    /// <param name="">Nenhum parâmetro é necessário</param>
    /// <returns>IActionResullt</returns>
    /// <response code='200'> Caso recuperação com sucesso</response>response>
    [HttpGet]
    public IActionResult RecuperaProfessor([FromQuery] int skip = 0,[FromQuery] int take = 10)
    {
        IEnumerable<ReadProfessorDto> readDto = _professorService.RecuperaProfessor(skip, take);
        if (readDto != null) return Ok(readDto);
        return NotFound();
    }

    /// <summary>
    /// Recupera objeto Professor de id específico do bando de dados
    /// </summary>
    /// <param name="id">Id do objeto Professor</param>
    /// <returns>IActionResullt</returns>
    /// <response code='200'> Caso recuperação com sucesso</response>response>
    [HttpGet("{id}")]
    public IActionResult RecuperaProfessorPorId(int id)
    {
        //var professor = professores.FirstOrDefault(professor => professor.Id == id);
        ReadProfessorDto readDto = _professorService.RecuperaReadProfessorId(id);
        if (readDto != null) return Ok(readDto);
        return NotFound();
    }

    /// <summary>
    /// Atualiza um objeto Professor de id específico do bando de dados
    /// </summary>
    /// <param name="id">Id do objeto Professor e Objeto com os parâmetros necessários para a atualização de um professor</param>
    /// <returns>IActionResullt</returns>
    /// <response code='204'> Caso atualizar com sucesso</response>response>
    [HttpPut("{id}")]
    public IActionResult AtualizaProfessor(int id, [FromBody] UpdateProfessorDto professorDto)
    {
        Result resultado = _professorService.AtualizaProfessor(id, professorDto);
        if (resultado.IsFailed) return NotFound();
        return NoContent();

    }

    /// <summary>
    /// Atualiza um atributo específico de um objeto Professor de id específico do bando de dados
    /// </summary>
    /// <param name="id">Id do objeto Professor e Objeto JSON com os parâmetros necessários para o update de um atributo específica do objeto professor</param>
    /// <returns>IActionResullt</returns>
    /// <response code='204'> Caso atualização com sucesso</response>response>
    [HttpPatch("{id}")]
    public IActionResult AtualizaProfessorPatch(int id,JsonPatchDocument<UpdateProfessorDto> patch)
    {
        UpdateProfessorDto updateDto = ValidateModelProfessor(id, patch);
        if (updateDto == null) return ValidationProblem(ModelState);

        Result resultadoAtualiza = _professorService.AtualizaProfessor(id, updateDto);
        if (resultadoAtualiza == null) return NotFound();
        return NoContent();
    }

    public UpdateProfessorDto ValidateModelProfessor(int id, JsonPatchDocument<UpdateProfessorDto> patch)
    {
        UpdateProfessorDto updateDto = _professorService.RecuperaUpdateProfessorId(id);
        if (updateDto == null) return null;

        patch.ApplyTo(updateDto, ModelState);

        if (!TryValidateModel(updateDto))
        {
            return null;
        }
        return updateDto;
    }

    /// <summary>
    /// Deleta um objeto Professor de id específico do bando de dados
    /// </summary>
    /// <param name="id">Id do objeto Professor</param>
    /// <returns>IActionResullt</returns>
    /// <response code='204'> Caso deleção com sucesso</response>response>
    [HttpDelete("{id}")]
    public IActionResult DeletaProfessor(int id)
    {
        Result resultado = _professorService.DeletaProfessor(id);
        if (resultado.IsFailed) return NotFound();
        return NoContent();
    }

}
 