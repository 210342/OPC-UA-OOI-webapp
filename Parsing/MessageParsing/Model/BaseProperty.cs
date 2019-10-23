using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public abstract class BaseProperty : IProperty
    {
        public object Value { get; set; }
        public IPropertyTemplate Template { get; } 

        internal BaseProperty(object value, IPropertyTemplate template)
        {
            Value = value;
            Template = template;
        }
    }
}
