﻿using ReactiveHMI.M2MCommunication.Core.CommonTypes;
using System.ComponentModel;

namespace ReactiveHMI.M2MCommunication.Core.Interfaces
{
    public interface ISubscription
    {
        UaTypeMetadata UaTypeMetadata { get; }
        string TypeAlias { get; }
        object Value { get; set; }
        void Enable(PropertyChangedEventHandler handler);
        void Disable();
        void InvokeValueUpdated();
    }
}
