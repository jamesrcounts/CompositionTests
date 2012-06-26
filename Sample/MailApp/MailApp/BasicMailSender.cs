using System.ComponentModel.Composition;
using System.Net.Mail;

namespace MailApp
{
    [Export]
    public class BasicMailSender : IBasicMailSender
    {
        private readonly SmtpHost host;
        private readonly Port port;

        [ImportingConstructor]
        public BasicMailSender(SmtpHost host, Port port)
        {
            this.port = port;
            this.host = host;
        }

        public void SendMessage(MailMessage message)
        {
            using (var client = new SmtpClient(host.HostName, port.PortNumber))
            {
                client.Send(message);
            }
        }
    }
}