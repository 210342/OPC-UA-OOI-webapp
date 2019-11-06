using MessageParsing.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageParsing
{
    public interface IMessageParser : IDisposable
    {
        IEnumerable<DrawableProperty> DrawableProperties { get; }
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        void Initialise(Func<Task> handler);
        Task InitialiseAsync(Func<Task> handler);
    }
}
