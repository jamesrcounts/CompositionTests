using System.Net.Mail;

namespace MailApp
{
    public interface IBasicMailSender
    {
        void SendMessage(MailMessage message);
    }
}