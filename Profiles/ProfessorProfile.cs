using AutoMapper;
using ProfessoresApi.Data.Dtos;
using ProfessoresApi.Models;

namespace ProfessoresApi.Profiles;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<CreateProfessorDto, Professor>();
        CreateMap<UpdateProfessorDto, Professor>();
        CreateMap<Professor, UpdateProfessorDto>();
        CreateMap<Professor, ReadProfessorDto>();
    }
}
