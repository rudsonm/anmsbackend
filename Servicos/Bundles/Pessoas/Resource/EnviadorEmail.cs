using System;
using System.Net.Mail;

namespace Servicos.Bundles
{
    //https://msdn.microsoft.com/pt-br/library/system.net.mail.smtpclient(v=vs.110).aspx
    public static class EnviadorEmail
    {
        const string HOST_SMTP = "smtp.gmail.com";
        const int PORTA_SMTP = 587;
        const bool USA_AUTENTICACAO = true;
        const string ENDERECO_ENVIO = "animaizinhoscorporation@gmail.com";
        const string USUARIO = "animaizinhoscorporation@gmail.com";
        const string SENHA = "emailridiculo2018";

        public static void Enviar(string enderecosDestino, string titulo, string mensagem)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(HOST_SMTP, PORTA_SMTP);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(USUARIO, SENHA);

                MailAddress from = new MailAddress(ENDERECO_ENVIO, "Animaiszinhos");

                MailMessage message = new MailMessage(ENDERECO_ENVIO, enderecosDestino);
                message.Body = mensagem;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = titulo;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}