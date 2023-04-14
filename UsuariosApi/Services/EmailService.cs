using MailKit.Net.Smtp;
using MimeKit;
using System.Reflection.Metadata.Ecma335;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class EmailService
{
    private IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
    {
        Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, code);
        var mensagemDeEmail = CriaCorpoDoEMail(mensagem);
        EnviarEmail(mensagemDeEmail);
    }

    private void EnviarEmail(MimeMessage mensagemDeEmail)
    {
        using(var client = new SmtpClient())
        {
            try
            {
                client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                //client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                client.Send(mensagemDeEmail);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }

    private MimeMessage CriaCorpoDoEMail(Mensagem mensagem)
    {
        var mensagemDeEmail = new MimeMessage();
        mensagemDeEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:From")));
        mensagemDeEmail.To.AddRange(mensagem.Destinatario);
        mensagemDeEmail.Subject = mensagem.Assunto;
        mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = mensagem.Conteudo
        };

        return mensagemDeEmail;
    }
}
