using MVCProject.DAL.Models.Shared;
using System.Net;
using System.Net.Mail;


namespace MVCProject.BLL.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            //enable SSl and Tls
            client.EnableSsl = true;
            //sender,reciver
            client.Credentials = new NetworkCredential("elsadanyahmed341@gmail.com", "poykyxewnnbhbuvl");
            client.Send("elsadanyahmed341@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
