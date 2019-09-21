using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface IMessage
    {
        public static Guid StaticTypeGuid { get; }
        public Guid Id { get; }
        public Guid TypeGuid { get => StaticTypeGuid; }
        public string TypeAsString { get; }
        public string Content { get; }
        public DateTime TimeSent { get; }
        public int Size { get; }
    }
}
