﻿using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public delegate void NewMessageEventHandler(IMessage message);

    public interface IMessageBus
    {
        public event NewMessageEventHandler NewMessage;
        public void SendMessage(IMessage message);
        public IEnumerable<IMessage> ReadOldMessages();
        public IEnumerable<IMessage> ReadOldMessagesByType(Type type);
        public IEnumerable<IMessage> ReadOldMessagesByTypeGuid(Guid typeGuid);
        public IEnumerable<IMessage> ReadOldMessagesBySubscription(ISubscription subscription);
    }
}
