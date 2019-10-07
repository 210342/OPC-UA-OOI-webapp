using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M2MCommunication;
using Microsoft.Extensions.Localization;

namespace MessageParsing
{
    public abstract class MessageParser : IMessageParser
    {
        protected ICollection<IProperty> Properties { get; } = new List<IProperty>();
        public IEnumerable<DrawableProperty> DrawableProperties => Properties.OfType<DrawableProperty>();
        public IEnumerable<PrintableProperty> PrintableProperties => Properties.OfType<PrintableProperty>();

        public abstract void Parse(IMessage message);
    }
}
