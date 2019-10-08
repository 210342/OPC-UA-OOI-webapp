using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public abstract class BaseProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Color? FontColor { get; set; }
    }
}
