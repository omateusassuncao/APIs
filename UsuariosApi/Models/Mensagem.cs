using MimeKit;

namespace UsuariosApi.Models;

public class Mensagem
{
    public List<MailboxAddress> Destinatario { get; set; }
    public string Assunto { get; set; }
    public string Conteudo { get; set; }
    public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo)
    {
        Destinatario = new List<MailboxAddress>();
        Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d,d)));
        Assunto = assunto;
        Conteudo = $"https://localhost:7198/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";

    }
}
