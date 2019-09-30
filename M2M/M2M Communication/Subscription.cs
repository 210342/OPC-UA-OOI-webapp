using System;

namespace M2M_Communication
{
    public class Subscription : ISubscription
    {
        public Guid TypeId { get; }
        public string TypeName { get; }

        public Subscription(Guid typeGuid)
        {
            TypeId = typeGuid;
        }

        public Subscription(Guid typeGuid, string typeName)
        {
            TypeId = typeGuid;
            TypeName = typeName;
        }

        public override bool Equals(object obj)
        {
            return TypeId.Equals((obj as Subscription)?.TypeId);
        }

        public override int GetHashCode()
        {
            return TypeId.GetHashCode();
        }
    }
}
