using System;
using M2MCommunication.Core;
using UAOOI.Configuration.Networking.Serialization;

namespace M2MCommunication
{
    public class Subscription : ISubscription
    {
        private object _value;

        public UATypeInfo TypeInfo { get; }
        public object Value 
        { 
            get => _value;
            set
            {
                _value = value;
                ValueUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ValueUpdated;

        public Subscription(UATypeInfo typeInfo, object value, EventHandler handler)
        {
            TypeInfo = typeInfo;
            Value = value;
            ValueUpdated += handler;
        }
    }
}
