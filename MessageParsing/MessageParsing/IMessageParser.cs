using MessageParsing.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageParsing
{
    public interface IMessageParser
    {
        IEnumerable<DrawableProperty> DrawableProperties { get; }
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        void Initialise();
        Task InitialiseAsync();
    }
}
