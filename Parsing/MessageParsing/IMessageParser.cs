using M2MCommunication;
using MessageParsing.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using M2MCommunication.Core;

namespace MessageParsing
{
    public interface IMessageParser
    {
        IEnumerable<DrawableProperty> DrawableProperties { get; }
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        void Parse();
        Task ParseAsync();
    }
}
