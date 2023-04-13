using System.ComponentModel.DataAnnotations;

namespace ProfessoresApi.Data.Dtos;

public class UpdateProfessorDto
{

    [Required(ErrorMessage = "O nome do Professor é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O CPF do Professor é obrigatório")]
    [Range(10000000000, 99999999999, ErrorMessage = "O CPF deve conter 11 caracteres. Não utilize pontos outraços, apenas números.Ex:12345678910")]
    public ulong CPF { get; set; }

    [StringLength(50, ErrorMessage ="O tamanho máximo da disciplina é 50 caractéres")]
    [Required(ErrorMessage = "A Disciplina do Professor é obrigatório")]
    public string Disciplina { get; set; }
}
