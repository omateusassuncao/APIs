using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CadastroController : ControllerBase
{
    private CadastroService _cadastroService;

    public CadastroController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpPost]
    public IActionResult CadastrarUsuario(CreateUsuarioDto createDto)
    {
        Result resultado = _cadastroService.CadastroUsuario(createDto);
        if (resultado.IsFailed) return StatusCode(500);
        return Ok();
    }
}