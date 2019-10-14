using M2MCommunication;
using MessageParsing.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageParsing
{
    public delegate void ObjectUpdatedEventHandler(object viewObject, IMessage message);

    public interface IMessageParser
    {
        public IEnumerable<DrawableProperty> DrawableProperties { get; }
        public IEnumerable<PrintableProperty> PrintableProperties { get; }

        public void Parse(IMessage message);
        public Task ParseAsync(IMessage message);
    }
}
