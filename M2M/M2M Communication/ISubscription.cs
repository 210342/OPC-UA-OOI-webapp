using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication
{
    public interface ISubscription
    {
        Guid TypeId { get; }
        string TypeName { get; }
    }
}
