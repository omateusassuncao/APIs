using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProfessoresApi.Data;
using ProfessoresApi.Data.Dtos;
using ProfessoresApi.Models;

namespace ProfessoresApi.Services;

public class ProfessorService
{

    private ProfessorContext _context;
    private IMapper _mapper;

    public ProfessorService(ProfessorContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadProfessorDto AdicionaProfessor(CreateProfessorDto professorDto)
    {
        Professor professor = _mapper.Map<Professor>(professorDto);
        _context.Professores.Add(professor);
        _context.SaveChanges();
        return _mapper.Map<ReadProfessorDto>(professor);
    }

    public IEnumerable<ReadProfessorDto> RecuperaProfessor(int skip, int take)
    {
        return _mapper.Map<List<ReadProfessorDto>>(_context.Professores.Skip(skip).Take(take));
    }

    public ReadProfessorDto RecuperaReadProfessorId(int id)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return null;
        return _mapper.Map<ReadProfessorDto>(professor);
    }

    public UpdateProfessorDto RecuperaUpdateProfessorId(int id)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return null;
        return _mapper.Map<UpdateProfessorDto>(professor);
    }

    public Result AtualizaProfessor(int id, UpdateProfessorDto professorDto)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return Result.Fail("Professor não encontrado");

        _mapper.Map(professorDto, professor);
        _context.SaveChanges();
        return Result.Ok();

    }

    //public Result AtualizaProfessorPatch(int id, JsonPatchDocument<UpdateProfessorDto> patch)
    //{
    //    var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
    //    if (professor == null) return Result.Fail("Professor não encontrado");

    //    var professorParaAtualizar = _mapper.Map<UpdateProfessorDto>(professor);

    //    _mapper.Map(professorParaAtualizar, professor);
    //    _context.SaveChanges();
    //    return Result.Ok();
    //}

    public Result DeletaProfessor(int id)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
        if (professor == null) return Result.Fail("Professor não encontrado");

        _context.Remove(professor);
        _context.SaveChanges();
        return Result.Ok();
    }
}
