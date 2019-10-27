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
        IEnumerable<DrawableProperty> DrawableProperties { get; }
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        void Parse(IMessage message);
        Task ParseAsync(IMessage message);
    }
}
