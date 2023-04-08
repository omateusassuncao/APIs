using System.ComponentModel.DataAnnotations;

namespace ProfessoresApi.Data.Dtos;

public class ReadProfessorDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ulong CPF { get; set; }
    public string Disciplina { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;

}
