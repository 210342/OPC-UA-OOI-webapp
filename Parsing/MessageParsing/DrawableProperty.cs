using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public class DrawableProperty : BaseProperty, IProperty
    {
        public decimal? XPosition { get; set; }
        public decimal? YPosition { get; set; }
        public Color? BackgroundColor { get; set; }

        public DrawableProperty(string name, object value, decimal? xPosition, decimal? yPosition, Color? backgroundColor, Color? fontColor)
        {
            Name = name;
            Value = value;
            XPosition = xPosition;
            YPosition = yPosition;
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
        }

        public PrintableProperty MapToPrintable()
        {
            return new PrintableProperty(Name, Value, FontColor);
        }
    }
}
