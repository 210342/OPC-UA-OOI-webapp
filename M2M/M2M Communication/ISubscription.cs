using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication
{
    public interface ISubscription
    {
        public Guid TypeId { get; }
        public string TypeName { get; }
    }
}
