using M2MCommunication.Core;
using System;
using System.ComponentModel;

namespace MessageParsingUnitTest
{
    internal class TestSubscription : ISubscription
    {
        private string _value;

        public object Value { get => _value; set { _value = value as string; ValueUpdated?.Invoke(this, EventArgs.Empty); } }

        public string TypeName => throw new NotImplementedException();

        public event EventHandler ValueUpdated;

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable(PropertyChangedEventHandler handler)
        {
            throw new NotImplementedException();
        }
    }
}
