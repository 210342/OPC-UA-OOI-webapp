using M2MCommunication.Core;
using System.Drawing;

namespace MessageParsing.Model
{
    public class PrintableProperty : BaseProperty
    {
        public PrintableProperty(ISubscription subscription, IPropertyTemplate template) : base(subscription, template) { }

        public DrawableProperty MapToDrawable(Point location, Color backgroundColor)
        {
            return new DrawableProperty(Subscription, new PropertyTemplate(Template.Name, location, Template.FontColor, backgroundColor));
        }
    }
}
