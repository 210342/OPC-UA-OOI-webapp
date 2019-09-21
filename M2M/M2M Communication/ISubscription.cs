using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface ISubscription
    {
        public ICollection<(Guid, Type)> Types { get; } 
    }
}
