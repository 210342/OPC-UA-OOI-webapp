using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialize();
    }
}
