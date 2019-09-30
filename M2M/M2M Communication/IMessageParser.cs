using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public delegate void ObjectUpdatedEventHandler(object viewObject, IMessage message);

    public interface IMessageParser
    {
        public event ObjectUpdatedEventHandler ObjectUpdated;
        public void Parse(IMessage message);
    }
}
