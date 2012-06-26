using System.ComponentModel.Composition;

namespace MailApp
{
    [Export(typeof(Port))]
    public class DefaultPort : Port
    {
        public DefaultPort() :
            base(25)
        {
        }
    }
}