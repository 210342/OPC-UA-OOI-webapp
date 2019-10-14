using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public class PrintableProperty : BaseProperty
    {
        public PrintableProperty(object value, IPropertyTemplate template) : base(value, template) { }

        public DrawableProperty MapToDrawable(Point location, Color backgroundColor)
        {
            return new DrawableProperty(Value, new PropertyTemplate(Template.Name, location, Template.FontColor, backgroundColor));
        }
    }
}
