using System.ComponentModel;

namespace M2MCommunication.Core
{
    public interface ISubscription
    {
        UaTypeMetadata UaTypeMetadata { get; }
        object Value { get; set; }
        void Enable(PropertyChangedEventHandler handler);
        void Disable();
    }
}
