using M2MCommunication.Core;
using System;
using System.ComponentModel;
using UAOOI.Configuration.Networking.Serialization;

namespace M2MCommunication.Uaooi
{
    public class Subscription : ISubscription
    {
        private object _value;

        public UaTypeMetadata UaTypeMetadata { get; }
        public UATypeInfo TypeInfo { get; }
        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueUpdated?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        internal event PropertyChangedEventHandler ValueUpdated;

        public Subscription(UATypeInfo typeInfo, UaTypeMetadata uaTypeMetadata, object value)
        {
            TypeInfo = typeInfo;
            UaTypeMetadata  = uaTypeMetadata?? throw new ArgumentNullException(nameof(uaTypeMetadata));
            Value = value;
        }

        public void Enable(PropertyChangedEventHandler handler)
        {
            Disable();
            ValueUpdated += handler;
        }

        public void Disable()
        {
            ValueUpdated = null;
        }

        public override string ToString()
        {
            try
            {
                return string.IsNullOrEmpty(Value?.ToString()) ? "null" : Value?.ToString();
            }
            catch (NullReferenceException)
            {
                return "null";
            }
        }
    }
}
