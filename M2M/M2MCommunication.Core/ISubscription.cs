using System.ComponentModel;

namespace M2MCommunication.Core
{
    public interface ISubscription
    {
        string TypeName { get; }
        object Value { get; set; }
        void Enable(PropertyChangedEventHandler handler);
        void Disable();
    }
}
