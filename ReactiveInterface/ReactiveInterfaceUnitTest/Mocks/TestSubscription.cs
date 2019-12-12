using M2MCommunication.Core.CommonTypes;
using M2MCommunication.Core.Interfaces;
using System.ComponentModel;

namespace ReactiveInterfaceUnitTest.Mocks
{
    internal class TestSubscription : ISubscription
    {
        private string _value;

        public object Value { get => _value; set { _value = value as string; ValueUpdated?.Invoke(this, null); } }

        public UaTypeMetadata UaTypeMetadata { get; set; }

        public string TypeAlias { get; } = "Alias";

        public event PropertyChangedEventHandler ValueUpdated;

        public void Disable()
        {
            ValueUpdated = null;
        }

        public void Enable(PropertyChangedEventHandler handler)
        {
            ValueUpdated += handler;
        }
    }
}
