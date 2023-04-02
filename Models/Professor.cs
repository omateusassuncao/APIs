using System.ComponentModel.DataAnnotations;

namespace ProfessoresApi.Models;

public class Professor
{
    [Required(ErrorMessage = "O nome do Professor é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O CPF do Professor é obrigatório")]
    [Range(10000000000, 99999999999, ErrorMessage = "O CPF deve conter 11 caracteres. Não utilize pontos outraços, apenas números.Ex:12345678910")]
    public ulong CPF { get; set; }

    [Required(ErrorMessage = "A Disciplina do Professor é obrigatório")]
    public string Disciplina { get; set; }
}
