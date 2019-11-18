using InterfaceModel.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageParsing
{
    public interface IMessageParser : IDisposable
    {
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        Task InitialiseAsync(Func<Task> handler);
    }
}
