using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public class PrintableProperty : BaseProperty, IProperty
    {
        public decimal? XPosition => null; 
        public decimal? YPosition => null; 
        public Color? BackgroundColor => null; 

        public PrintableProperty(string name, object value, Color? fontColor)
        {
            Name = name;
            Value = value;
            FontColor = fontColor;
        }

        public DrawableProperty MapToDrawable(decimal xPosition, decimal yPosition, Color backgroundColor)
        {
            return new DrawableProperty(Name, Value, xPosition, yPosition, backgroundColor, FontColor);
        }
    }
}
