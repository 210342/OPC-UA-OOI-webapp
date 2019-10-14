using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public interface IProperty
    {
        object Value { get; }
        IPropertyTemplate Template { get; }
    }
}
