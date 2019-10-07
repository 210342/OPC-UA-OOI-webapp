using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public class DrawableProperty : IProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public decimal? XPosition { get; set; }
        public decimal? YPosition { get; set; }
        public Color? BackgroundColor { get; set; }
        public Color? FontColor { get; set; }

        public DrawableProperty(string name, object value, decimal xPosition, decimal yPosition, Color backgroundColor, Color fontColor)
        {
            Name = name;
            Value = value;
            XPosition = xPosition;
            YPosition = yPosition;
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
        }
    }
}
