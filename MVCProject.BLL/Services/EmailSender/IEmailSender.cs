

using MVCProject.DAL.Models.Shared;

namespace MVCProject.BLL.Services.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
