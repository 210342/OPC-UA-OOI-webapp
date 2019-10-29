using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public interface IProperty
    {
        ISubscription Subscription { get; }
        IPropertyTemplate Template { get; }
    }
}
