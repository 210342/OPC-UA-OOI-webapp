using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication
{
    public interface IMessage
    {
        Guid Id { get; }
        Guid TypeGuid { get; }
        string TypeAsString { get; }
        string Content { get; }
        DateTime TimeSent { get; }
        int Size { get; }
    }
}
