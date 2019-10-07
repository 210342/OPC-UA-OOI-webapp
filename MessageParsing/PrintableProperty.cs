using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public class PrintableProperty : IProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public decimal? XPosition => null;
        public decimal? YPosition => null;
        public Color? BackgroundColor => null;
        public Color? FontColor { get; set; }

        public PrintableProperty(string name, object value, Color fontColor)
        {
            Name = name;
            Value = value;
            FontColor = fontColor;
        }
    }
}
