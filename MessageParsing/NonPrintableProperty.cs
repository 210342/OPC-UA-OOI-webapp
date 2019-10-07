using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public class NonPrintableProperty : IProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public decimal? XPosition => null;
        public decimal? YPosition => null;
        public Color? BackgroundColor => null;
        public Color? FontColor => null;

        public NonPrintableProperty(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
