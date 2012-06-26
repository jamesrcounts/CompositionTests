namespace MailApp
{
    public abstract class Port
    {
        protected Port(int portNumber)
        {
            this.PortNumber = portNumber;
        }

        public int PortNumber { get; private set; }
    }
}