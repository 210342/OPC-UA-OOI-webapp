using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2MCommunication;

namespace MessageParsing
{
    public abstract class MessageParser : IMessageParser
    {
        protected ICollection<IProperty> Properties { get; } = new List<IProperty>();
        public IEnumerable<DrawableProperty> DrawableProperties => Properties?.OfType<DrawableProperty>();
        public IEnumerable<PrintableProperty> PrintableProperties => Properties?.OfType<PrintableProperty>();

        public abstract void Parse(IMessage message);
        public abstract Task ParseAsync(IMessage message);
    }
}
