using M2MCommunication;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageParsing
{
    public delegate void ObjectUpdatedEventHandler(object viewObject, IMessage message);

    public interface IMessageParser
    {
        public IEnumerable<DrawableProperty> DrawableProperties { get; }
        public IEnumerable<PrintableProperty> PrintableProperties { get; }

        public void Parse(IMessage message);
    }
}
