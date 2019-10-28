using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageParsingUnitTest
{
    internal class TestSubscription : ISubscription
    {
        private string _value;

        public object Value { get => _value; set { _value = value as string; ValueUpdated?.Invoke(this, EventArgs.Empty); } }

        public event EventHandler ValueUpdated;
    }
}
