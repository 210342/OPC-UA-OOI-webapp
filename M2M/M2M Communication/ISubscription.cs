using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface ISubscription
    {
        public Guid TypeId { get; }
        public NewMessageEventHandler NewMessage { get; set; }
    }
}
