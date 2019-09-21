using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface IMessage
    {
        public Guid ID { get; }
        public Guid TypeGuid { get; }
        public string TypeAsString { get; }
        public string Content { get; }
        public DateTime TimeSent { get; }
        public int Size { get; }
    }
}
