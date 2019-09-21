using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface IMessageParser
    {
        public void Parse(IMessage message);
    }
}
