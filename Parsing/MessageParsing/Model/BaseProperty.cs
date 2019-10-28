using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public abstract class BaseProperty : IProperty
    {
        public ISubscription Subscription { get; }
        public IPropertyTemplate Template { get; } 

        internal BaseProperty(ISubscription subsctiption, IPropertyTemplate template)
        {
            Subscription = subsctiption;
            Template = template;
        }
    }
}
