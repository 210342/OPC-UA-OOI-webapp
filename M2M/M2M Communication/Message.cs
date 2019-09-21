using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public class Message : IMessage
    {
        private readonly static Guid _id = new Guid("E6702AFB-6CA3-4540-AC5D-B2BABACA14AE");

        public Guid ID { get; } = Guid.NewGuid();

        public Guid TypeGuid { get => _id; }

        public string TypeAsString => nameof(Message);

        public string Content => ID.ToString();

        public DateTime TimeSent { get; } = DateTime.UtcNow;

        public int Size { get => Content.Length; }
    }
}
