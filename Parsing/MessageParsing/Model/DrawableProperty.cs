using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public class DrawableProperty : BaseProperty
    {
        public DrawableProperty(object value, IPropertyTemplate template) : base (value, template) { }

        public PrintableProperty MapToPrintable()
        {
            return new PrintableProperty(Value, new PropertyTemplate(Template.Name, null, Template.FontColor, null));
        }
    }
}
