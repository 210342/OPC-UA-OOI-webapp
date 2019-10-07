using System;

namespace M2MCommunication
{
    public class Message : IMessage
    {
        public static Guid StaticTypeGuid { get => new Guid("E6702AFB-6CA3-4540-AC5D-B2BABACA14AE"); }

        public Guid Id { get; } = Guid.NewGuid();
        public Guid TypeGuid => StaticTypeGuid;
        public string TypeAsString => nameof(Message);
        public string Content => Id.ToString();
        public DateTime TimeSent { get; } = DateTime.UtcNow;
        public int Size { get => Content.Length; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Id.Equals((obj as Message)?.Id);
        }
    }
}
