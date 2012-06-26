namespace MailApp
{
    public abstract class SmtpHost
    {
        protected SmtpHost(string hostName)
        {
            this.HostName = hostName;
        }

        public string HostName { get; private set; }
    }
}