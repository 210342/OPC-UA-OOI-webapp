using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public class Subscription : ISubscription
    {
        public ICollection<(Guid, Type)> Types { get; } = new List<(Guid, Type)>();

        public Subscription(Guid typeGuid, Type type)
        {
            Types.Add((typeGuid, type));
        }

        public Subscription(IEnumerable<(Guid, Type)> types)
        {
            Types = new List<(Guid, Type)>(types);
        }
    }
}
